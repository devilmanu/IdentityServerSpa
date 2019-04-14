using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityRightWay.Infrastructure.Bus.Commands
{
    public interface ICommand : IRequest { }

    public interface ICommand<TResponse> : IRequest<TResponse> { }
}
