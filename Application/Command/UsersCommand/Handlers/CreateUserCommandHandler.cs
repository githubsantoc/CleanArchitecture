using Application.Command.UsersCommand;
using Application.Wrapper;
using AutoMapper;
using Domains.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.UsersCommand.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IRoleWrapper _role;
        private readonly IUserWrapper _user;

        public CreateUserCommandHandler(IUserWrapper user, IRoleWrapper roleManager)
        {
            _role = roleManager;
            _user = user;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = createUser(request);
            //it is added to validate if user email already exist in db or not

            /*new RegisterValidator(_context).ValidateAndThrow(command); // Use Validate*/


            // Store user data in NetUsers database table
            var result = await _user.CreateUserAsync(user, request.Password);
            if (result.Succeeded && await _role.RoleExistsAsync(request.Role.ToString()))
                await _user.AddToRoleAsync(user, request.Role.ToString());
            return user;
        }

        public User createUser(CreateUserCommand command)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = command.email,
                UserName = command.Name
            };
            return user;
        }
    }

}

