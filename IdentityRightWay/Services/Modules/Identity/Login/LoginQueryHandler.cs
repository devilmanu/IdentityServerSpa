using IdentityRightWay.Domain.Entities;
using IdentityRightWay.Domain.Shared.Exceptions;
using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Services.Shared;
using MediatR;
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

        public LoginQueryHandler(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IdentityRightWayResponseBase<LoginDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(request.Email);
            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user,request.Password, false, false);
                if (result.Succeeded)
                    return new IdentityRightWayResponseBase<LoginDto> { Errors = null, IsValid = true, Payload = new LoginDto { IsLogged = true } };
                else if (result.IsLockedOut)
                    return new IdentityRightWayResponseBase<LoginDto> { Errors = new string[] { "User Locked" }, IsValid = false, Payload = new LoginDto { IsLogged = false } };
                else if (result.IsNotAllowed)
                    return new IdentityRightWayResponseBase<LoginDto> { Errors = new string[] { "User in not Allowed" }, IsValid = false, Payload = new LoginDto { IsLogged = false } };
                else if(result.RequiresTwoFactor)
                    return new IdentityRightWayResponseBase<LoginDto> { Errors = new string[] { "Requires two factor" }, IsValid = false, Payload = new LoginDto { IsLogged = false } };
                else
                    throw new IdentityRightWayException(HttpStatusCode.NotFound, "invalid credentials");
            }
            else
            {
                throw new IdentityRightWayException(HttpStatusCode.NotFound, "Email not found");
            }
        }
    }
}
