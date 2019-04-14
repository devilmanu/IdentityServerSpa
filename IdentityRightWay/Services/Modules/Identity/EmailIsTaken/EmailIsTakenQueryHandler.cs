using IdentityRightWay.Domain.Entities;
using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Services.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityRightWay.Services.Modules.Identity.EmailIsTaken
{
    public class EmailIsTakenQueryHandler : IQueryHandler<EmailIsTakenQuery, IdentityRightWayResponseBase<EmailIsTakenDto>>
    {
        private readonly UserManager<AppUser> _userManager;

        public EmailIsTakenQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityRightWayResponseBase<EmailIsTakenDto>> Handle(EmailIsTakenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new IdentityRightWayResponseBase<EmailIsTakenDto>
                {
                    Payload = new EmailIsTakenDto { IsTaken = false },
                    IsValid = true,
                    Errors = null
                };
            else
                return new IdentityRightWayResponseBase<EmailIsTakenDto>
                {
                    Payload = new EmailIsTakenDto { IsTaken = true },
                    IsValid = false,
                    Errors = new string[]{ "User Email Is Taken" }
                };
        }
    }
}
