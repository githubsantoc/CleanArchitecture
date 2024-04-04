using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CustomerCommand.Command
{
    public class UpdateCustomerCommand : IRequest<string>
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public bool Active { get; set; } = true;
    }
}
