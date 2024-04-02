using Domains.Entities;
using Domains.repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Handler
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, string>
    {
        private readonly ICustomerRepo _customerRepo;
        public CreateCustomerCommandHandler(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task<string> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var product = new Customer
            {
                Name = request.Name,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                UserId = request.UserId
            };

            await _customerRepo.AddAsync(product);
            return "created successfully";
        }
    }
}
