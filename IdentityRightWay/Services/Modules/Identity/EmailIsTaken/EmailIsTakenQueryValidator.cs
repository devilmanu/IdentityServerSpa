using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRightWay.Services.Modules.Identity.EmailIsTaken
{
    public class EmailIsTakenQueryValidator : AbstractValidator<EmailIsTakenQuery>
    {
        public EmailIsTakenQueryValidator()
        {
            RuleFor(o => o.Email).EmailAddress();
        }
    }
}
