using IdentityRightWay.Infrastructure.TypeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace IdentityRightWay.Domain.Shared.Exceptions
{
    [ExportTsInterface(OutputDir = ExportPathTsInterfaces.BasePath)]
    [TsIgnoreBase]
    public class IdentityRightWayException : Exception
    {
        public IdentityRightWayException(HttpStatusCode statusCode, params string[] errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
        [TsType(TsType.Number)]
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
