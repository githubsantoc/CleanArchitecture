
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command
{
    public class CreateCustomerCommand : IRequest<string>
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Active { get; set; } = true;
        public string? Product { get; set; }
        public required string UserId { get; set; }
    }
}
