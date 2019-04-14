using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRightWay.Services.Modules.Identity.EmailIsTaken
{
    public class EmailIsTakenQuery : IQuery<IdentityRightWayResponseBase<EmailIsTakenDto>>
    {
        public string Email { get; set; }
    }
}
