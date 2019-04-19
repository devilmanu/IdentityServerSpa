using IdentityRightWay.Domain.Shared.Exceptions;
using IdentityRightWay.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRightWay.Api.Shared
{
    public class IdentityRightWayExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is IdentityRightWayException)
            {
                var ex = context.Exception as IdentityRightWayException;
                context.Result = new ObjectResult(
                    new IdentityRightWayResponseBase<IdentityRightWayException>
                    {
                        Errors = ex.Errors.ToArray(),
                        IsValid = false,
                        Payload = null,
                        TotalCount = null
                    }
                );
                context.HttpContext.Response.StatusCode = (int)ex.StatusCode;
            }
        }
    }
}
