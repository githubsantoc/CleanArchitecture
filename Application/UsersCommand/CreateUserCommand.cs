using Domains.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersCommand
{
    public enum Role { Admin, Customer };
    public class CreateUserCommand :IRequest<User>
    {
            public string? email { get; set; }
            public string? Name { get; set; }
            public string? Password { get; set; }
            public Role? Role { get; set; }
    }
}
