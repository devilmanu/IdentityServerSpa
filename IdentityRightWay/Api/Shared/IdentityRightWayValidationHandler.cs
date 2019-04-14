using IdentityRightWay.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRightWay.Api.Shared
{
    public class IdentityRightWayValidationHandler : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(
                    new IdentityRightWayResponseBase<string>
                    {
                        Errors = context.ModelState.Values.SelectMany(o => o.Errors.Select(u => u.ErrorMessage)).ToList(),
                        IsValid = false,
                        Payload = null,
                        TotalCount = null
                    }
                );
            }
        }
    }
}
