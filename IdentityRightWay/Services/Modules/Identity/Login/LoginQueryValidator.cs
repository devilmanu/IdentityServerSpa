using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRightWay.Services.Modules.Identity.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(o => o.Email).NotEmpty().NotNull();
            RuleFor(o => o.Password).NotEmpty().NotNull();
        }
    }
}
