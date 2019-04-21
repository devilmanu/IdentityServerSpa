using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Infrastructure.IdentityServer4.Extensions;
using IdentityRightWay.Services.Modules.Consent.GetConsent;
using IdentityRightWay.Services.Shared;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityRightWay.Services.Modules.Consent.AcceptConsent
{
    public class AcceptConsentCommandHandler : IQueryHandler<AcceptConsentCommand, IdentityRightWayResponseBase<AcceptConsentDto>>
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly IEventService _events;
        private readonly ILogger<AcceptConsentCommandHandler> _logger;

        public AcceptConsentCommandHandler(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IResourceStore resourceStore,
            IEventService events,
            ILogger<AcceptConsentCommandHandler> logger)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _resourceStore = resourceStore;
            _events = events;
            _logger = logger;
        }

        public async Task<IdentityRightWayResponseBase<AcceptConsentDto>> Handle(AcceptConsentCommand request, CancellationToken cancellationToken)
        {
            var result = await ProcessConsent(request);

            if (result.IsRedirect)
            {
                if (await _clientStore.IsPkceClientAsync(result.ClientId))
                {
                    // if the client is PKCE then we assume it's native, so this change in how to
                    // return the response is for better UX for the end user.
                    //return View("Redirect", new RedirectViewModel { RedirectUrl = result.RedirectUri });
                    return new IdentityRightWayResponseBase<AcceptConsentDto> { Errors = null, IsValid = true, Payload = new AcceptConsentDto { RedirectUri = result.RedirectUri } };
                }

                return new IdentityRightWayResponseBase<AcceptConsentDto> { Errors = null, IsValid = false };
            }
            return new IdentityRightWayResponseBase<AcceptConsentDto> { Errors = null, IsValid = false };
        }

        private async Task<ProcessConsentResult> ProcessConsent(ConsentInputDto model)
        {
            var result = new ProcessConsentResult();

            // validate return url is still valid
            var request = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            if (request == null) return result;

            ConsentResponse grantedConsent = null;

            // user clicked 'no' - send back the standard 'access_denied' response
            if (model?.Button == "no")
            {
                grantedConsent = ConsentResponse.Denied;

                // emit event
                await _events.RaiseAsync(new ConsentDeniedEvent("", request.ClientId, request.ScopesRequested));
            }
            // user clicked 'yes' - validate the data
            else if (model?.Button == "yes")
            {
                // if the user consented to some scope, build the response model
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    var scopes = model.ScopesConsented;
                    if (ConsentOptions.EnableOfflineAccess == false)
                    {
                        scopes = scopes.Where(x => x != IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess);
                    }

                    grantedConsent = new ConsentResponse
                    {
                        RememberConsent = model.RememberConsent,
                        ScopesConsented = scopes.ToArray()
                    };

                    // emit event
                    await _events.RaiseAsync(new ConsentGrantedEvent("", request.ClientId, request.ScopesRequested, grantedConsent.ScopesConsented, grantedConsent.RememberConsent));
                }
                else
                {
                    result.ValidationError = ConsentOptions.MustChooseOneErrorMessage;
                }
            }
            else
            {
                result.ValidationError = ConsentOptions.InvalidSelectionErrorMessage;
            }

            if (grantedConsent != null)
            {
                // communicate outcome of consent back to identityserver
                await _interaction.GrantConsentAsync(request, grantedConsent);

                // indicate that's it ok to redirect back to authorization endpoint
                result.RedirectUri = model.ReturnUrl;
                result.ClientId = request.ClientId;
            }
            else
            {
                // we need to redisplay the consent UI
                throw new NotImplementedException("nada hay que implementar esto");
                //result.ViewModel = await BuildViewModelAsync(model.ReturnUrl, model);
            }

            return result;
        }
    }

    public class ProcessConsentResult
    {
        public bool IsRedirect => RedirectUri != null;
        public string RedirectUri { get; set; }
        public string ClientId { get; set; }

        public bool ShowView => ViewModel != null;
        public ConsentDto ViewModel { get; set; }

        public bool HasValidationError => ValidationError != null;
        public string ValidationError { get; set; }
    }

}
