using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRightWay.Services.Modules.Identity.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(o => o.OldPassword).NotEmpty().NotNull();
            RuleFor(o => o.NewPassword).NotEmpty().NotNull();
            RuleFor(o => o.NewPasswordConfirm).Equal(o => o.NewPassword);
            RuleFor(o => o.Email).NotEmpty().NotNull();
        }
    }
}
