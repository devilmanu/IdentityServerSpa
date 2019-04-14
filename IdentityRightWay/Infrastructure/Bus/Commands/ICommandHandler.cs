using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityRightWay.Infrastructure.Bus.Commands
{
    public interface ICommandHandler<T> : IRequestHandler<T>
        where T : ICommand
    {
    }

    public interface ICommandHandler<in T,R> : IRequestHandler<T,R>
    where T : ICommand<R>
    {
    }
}
