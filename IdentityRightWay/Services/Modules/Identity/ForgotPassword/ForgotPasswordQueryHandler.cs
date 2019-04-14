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

namespace IdentityRightWay.Services.Modules.Identity.ForgotPassword
{
    public class ForgotPasswordQueryHandler : IQueryHandler<ForgotPasswordQuery, IdentityRightWayResponseBase<ForgotPasswordDto>>
    {
        private readonly UserManager<AppUser> _userManager;

        public ForgotPasswordQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityRightWayResponseBase<ForgotPasswordDto>> Handle(ForgotPasswordQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                return new IdentityRightWayResponseBase<ForgotPasswordDto>
                {
                    Errors = null,
                    IsValid = true,
                    Payload = new ForgotPasswordDto { Token = token },
                };
            }
            else
            {
                throw new IdentityRightWayException("User not found", 404);
            }
        }
    }
}
