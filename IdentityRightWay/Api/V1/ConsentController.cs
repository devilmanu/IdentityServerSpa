using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityRightWay.Api.Shared;
using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Services.Modules.Consent.AcceptConsent;
using IdentityRightWay.Services.Modules.Consent.GetConsent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityRightWay.Api.V1
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConsentController : IdentityRightWayControllerBase
    {
        public ConsentController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
        {
        }


        // POST: api/Consent
        [HttpPost]
        public async Task<IActionResult> Accept([FromBody] AcceptConsentCommand value)
        {
            var response = await queryBus.Send(value);
            return Ok(response);
        }

        // GET: api/Consent
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetConsentQuery getConsentQuery)
        {
            var response = await queryBus.Send(getConsentQuery);
            return Ok(response);
        }

    }
}
