using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.TypeGen;
using IdentityRightWay.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace IdentityRightWay.Services.Modules.Identity.Register
{
    [ExportTsInterface(OutputDir = ExportPathTsInterfaces.BasePath)]
    [TsIgnoreBase]
    public class RegisterCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
    }
}
