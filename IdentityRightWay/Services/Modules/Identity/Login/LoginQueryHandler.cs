using IdentityRightWay.Domain.Entities;
using IdentityRightWay.Domain.Shared.Exceptions;
using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Infrastructure.IdentityServer4.Extensions;
using IdentityRightWay.Services.Shared;
using IdentityServer4.Configuration;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityRightWay.Services.Modules.Identity.Login
{
    public class LoginQueryHandler : IQueryHandler<LoginQuery, IdentityRightWayResponseBase<LoginDto>>
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IEventService _events;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IdentityServerOptions _identityServerOptions;

        public LoginQueryHandler(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,  // los clientes como js consola......
            IEventService events,
            IAuthenticationSchemeProvider schemeProvider,
            IdentityServerOptions identityServerOptions
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _events = events;
            _schemeProvider = schemeProvider;
            _identityServerOptions = identityServerOptions;
        }

        public async Task<IdentityRightWayResponseBase<LoginDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var context = await _interaction.GetAuthorizationContextAsync(request.ReturnUrl);
            var user = await _signInManager.UserManager.FindByEmailAsync(request.Email);
            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user,request.Password, false, false);
                if (result.Succeeded)
                {
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName));
                    var claims = await _userManager.GetClaimsAsync(user);
                    if (context != null)
                    {
                        if (await _clientStore.IsPkceClientAsync(context.ClientId))
                        {
                            // if the client is PKCE then we assume it's native, so this change in how to
                            // return the response is for better UX for the end user.
                            return new IdentityRightWayResponseBase<LoginDto> { Errors = null, IsValid = true, Payload = new LoginDto { ReturnUrl = request.ReturnUrl } };
                        }

                        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                        return new IdentityRightWayResponseBase<LoginDto> { Errors = null, IsValid = true, Payload = new LoginDto { ReturnUrl = request.ReturnUrl } };
                    }

                    return new IdentityRightWayResponseBase<LoginDto> { Errors = null, IsValid = true, Payload = new LoginDto { ReturnUrl = request.ReturnUrl } };
                }
                else
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(request.Email, "invalid credentials"));
                    throw new IdentityRightWayException(HttpStatusCode.OK, "invalid credentials");

                }
            }
            else
            {
                await _events.RaiseAsync(new UserLoginFailureEvent(request.Email, "invalid credentials"));
                throw new IdentityRightWayException(HttpStatusCode.OK, "Email not found");
            }
        }

        private async Task<LoginViewModel> Test(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null ||
                            (x.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase))
                )
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }
    }

    internal class LoginViewModel
    {
        public object AllowRememberLogin { get; set; }
        public bool EnableLocalLogin { get; set; }
        public string ReturnUrl { get; set; }
        public string Username { get; set; }
        public object ExternalProviders { get; set; }
    }

    public class ExternalProvider
    {
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }
    }

    public class AccountOptions
    {
        public static bool AllowLocalLogin = true;
        public static bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public static bool ShowLogoutPrompt = true;
        public static bool AutomaticRedirectAfterSignOut = false;

        // specify the Windows authentication scheme being used
        public static readonly string WindowsAuthenticationSchemeName = Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme;
        // if user uses windows auth, should we load the groups from windows
        public static bool IncludeWindowsGroups = false;

        public static string InvalidCredentialsErrorMessage = "Invalid username or password";

        public static string UserAlreadyExistsErrorMessage = "User already exists";
    }
}
