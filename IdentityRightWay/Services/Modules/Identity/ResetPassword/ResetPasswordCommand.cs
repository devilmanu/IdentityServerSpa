using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.TypeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace IdentityRightWay.Services.Modules.Identity.ResetPassword
{
    [ExportTsInterface(OutputDir = ExportPathTsInterfaces.BasePath)]
    [TsIgnoreBase]
    public class ResetPasswordCommand : ICommand
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}
