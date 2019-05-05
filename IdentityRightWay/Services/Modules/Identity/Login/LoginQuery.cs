using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Infrastructure.TypeGen;
using IdentityRightWay.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace IdentityRightWay.Services.Modules.Identity.Login
{
    [ExportTsInterface(OutputDir = ExportPathTsInterfaces.BasePath)]
    [TsIgnoreBase]
    public class LoginQuery : IQuery<IdentityRightWayResponseBase<LoginDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
