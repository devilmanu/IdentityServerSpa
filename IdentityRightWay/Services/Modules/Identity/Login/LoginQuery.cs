using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRightWay.Services.Modules.Identity.Login
{
    public class LoginQuery : IQuery<IdentityRightWayResponseBase<LoginDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
