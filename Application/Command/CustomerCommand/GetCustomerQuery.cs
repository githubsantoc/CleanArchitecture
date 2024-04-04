using Domains.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CustomerCommand
{
    public class GetCustomerQuery : IRequest<List<Customer>>
    {
    }
}
