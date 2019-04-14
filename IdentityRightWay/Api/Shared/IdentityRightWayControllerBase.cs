using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.Bus.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRightWay.Api.Shared
{
    public class IdentityRightWayControllerBase : ControllerBase
    {
        protected ICommandBus commandBus;
        protected IQueryBus queryBus;

        public IdentityRightWayControllerBase(ICommandBus commandBus, IQueryBus queryBus)
        {
            this.commandBus = commandBus;
            this.queryBus = queryBus;
        }
    }
}
