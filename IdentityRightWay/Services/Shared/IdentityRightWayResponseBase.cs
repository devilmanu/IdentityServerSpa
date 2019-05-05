using IdentityRightWay.Infrastructure.TypeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace IdentityRightWay.Services.Shared
{
    [ExportTsInterface(OutputDir = ExportPathTsInterfaces.BasePath)]
    public class IdentityRightWayResponseBase<T>
    {
        public int? TotalCount { get; set; }
        public T Payload { get; set; }
        public bool IsValid { get; set; }
        public ICollection<string> Errors { get; set; }
    }

}
