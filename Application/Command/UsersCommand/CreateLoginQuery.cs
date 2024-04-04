using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.UsersCommand
{
    public class CreateLoginQuery : IRequest<string>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class RefreshToken
    {
        public required string Token { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expired { get; set; }
    }
}
