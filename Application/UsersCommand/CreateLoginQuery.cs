
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersCommand
{
    public class CreateLoginQuery : IRequest<string>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
