using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IdentityRightWay.Domain.Shared.Exceptions
{
    public class IdentityRightWayException : Exception
    {
        public IdentityRightWayException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public IdentityRightWayException(HttpStatusCode statusCode, params string[] errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
