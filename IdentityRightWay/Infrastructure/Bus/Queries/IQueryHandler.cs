using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityRightWay.Infrastructure.Bus.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
           where TQuery : IQuery<TResponse>
    {
    }
}
