using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityRightWay.Infrastructure.Bus.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
