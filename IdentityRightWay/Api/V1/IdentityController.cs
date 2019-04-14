using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityRightWay.Api.Shared;
using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Services.Modules.Identity.ForgotPassword;
using IdentityRightWay.Services.Modules.Identity.Login;
using IdentityRightWay.Services.Modules.Identity.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityRightWay.Api.V1
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : IdentityRightWayControllerBase
    {
        public IdentityController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
        {
        }

        /// <summary>
        /// logs user
        /// </summary>
        /// <param name="loginQuery"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginQuery loginQuery)
        {
            var response = await queryBus.Send(loginQuery);
            return Ok(response);
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="registerCommand"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            await commandBus.Send(registerCommand);
            return Ok();
        }

        /// <summary>
        /// Request token for reset password
        /// </summary>
        /// <param name="forgotPassword"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ForgotPassword([FromQuery] ForgotPasswordQuery forgotPassword)
        {
            var response = await queryBus.Send(forgotPassword);
            return Ok(response);
        }
    }
}
