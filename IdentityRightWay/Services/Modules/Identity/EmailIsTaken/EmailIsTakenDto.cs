using IdentityRightWay.Infrastructure.TypeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace IdentityRightWay.Services.Modules.Identity.EmailIsTaken
{
    [ExportTsInterface(OutputDir = ExportPathTsInterfaces.BasePath)]
    public class EmailIsTakenDto
    {
        public bool IsTaken { get; set; }
    }
}
