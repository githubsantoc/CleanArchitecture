
using Application.Validation;
using Application.Wrapper;
using AutoMapper;
using Domains.Entities;
using FluentValidation;
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
        private readonly RegisterValidator _validator;

        public CreateUserCommandHandler(IUserWrapper user, IRoleWrapper roleManager, RegisterValidator validator)
        {
            _role = roleManager;
            _user = user;
            _validator = validator;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = createUser(request);
            //it is added to validate if user email already exist in db or not

            _validator.ValidateAndThrow(request); // Use Validate*/


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
                UserName = command.Name,
                PhoneNumber = command.PhoneNumber
            };
            return user;
        }
    }

}

