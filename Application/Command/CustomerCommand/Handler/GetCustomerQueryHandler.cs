using Domains.Entities;
using Domains.repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CustomerCommand.Handler
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, List<Customer>>
    {
        private readonly ICustomerRepo _customerRepo;
        public GetCustomerQueryHandler(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task<List<Customer>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepo.ToListAsy();
        }
    }
}
