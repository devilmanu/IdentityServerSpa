using IdentityRightWay.Infrastructure.TypeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace IdentityRightWay.Services.Modules.Identity.ForgotPassword
{
    [ExportTsInterface(OutputDir = ExportPathTsInterfaces.BasePath)]
    public class ForgotPasswordDto
    {
        public string Token { get; set; }
    }
}
