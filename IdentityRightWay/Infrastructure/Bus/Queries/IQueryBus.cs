using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IdentityRightWay.Infrastructure.Bus.Queries
{
    public interface IQueryBus
    {
        Task<TResponse> Send<TResponse>(IQuery<TResponse> query);
    }
}
