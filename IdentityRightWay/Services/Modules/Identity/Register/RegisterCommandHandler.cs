using IdentityRightWay.Domain.Entities;
using IdentityRightWay.Domain.Shared.Exceptions;
using IdentityRightWay.Infrastructure.Bus.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityRightWay.Services.Modules.Identity.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                var userToRegister = new AppUser
                {
                    Email = request.Email,
                    Id = request.Id,
                };
                var result = await _userManager.CreateAsync(userToRegister, request.Password);
                if (result.Succeeded)
                    return new Unit();
                else
                    throw new Exception(result.Errors.FirstOrDefault().Description);
            }
            else
            {
                throw new IdentityRightWayException("Email already exist", 400);
            }
        }
    }
}
