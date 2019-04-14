using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRightWay.Domain.Shared.Exceptions
{
    public class IdentityRightWayException : Exception
    {
        public IdentityRightWayException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}
