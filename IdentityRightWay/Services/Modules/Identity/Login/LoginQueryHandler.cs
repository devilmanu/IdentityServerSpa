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
using System.Threading;
using System.Threading.Tasks;

namespace IdentityRightWay.Services.Modules.Identity.Login
{
    public class LoginQueryHandler : IQueryHandler<LoginQuery, IdentityRightWayResponseBase<bool>>
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginQueryHandler(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IdentityRightWayResponseBase<bool>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(request.Email);
            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user,request.Password, false, false);
                if (result.Succeeded)
                    return new IdentityRightWayResponseBase<bool> { Errors = null, IsValid = true, Payload = true };
                else if (result.IsLockedOut)
                    return new IdentityRightWayResponseBase<bool> { Errors = new string[] { "User Locked" }, IsValid = false, Payload = false };
                else if (result.IsNotAllowed)
                    return new IdentityRightWayResponseBase<bool> { Errors = new string[] { "User in not Allowed" }, IsValid = false, Payload = false };
                else if(result.RequiresTwoFactor)
                    return new IdentityRightWayResponseBase<bool> { Errors = new string[] { "Requires two factor" }, IsValid = false, Payload = false };
                else
                    throw new IdentityRightWayException("User not found", 404);
            }
            else
            {
                throw new IdentityRightWayException("User not found", 404);
            }
        }
    }
}
