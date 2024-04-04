using Application.Command.CustomerCommand.Command;
using Application.Wrapper;
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
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, string>
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IUserWrapper _userManager;
        public CreateCustomerCommandHandler(ICustomerRepo customerRepo, IUserWrapper userManager)
        {
            _customerRepo = customerRepo;
            _userManager = userManager;
        }
        public async Task<string> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAs(request.Email);
            var customer = new Customer
            {
                Name = request.Name,
                Address = request.Address,
                PhoneNumber = user.PhoneNumber,
                UserId = user.Id
            };

            await _customerRepo.AddAsy(customer);
            return "created successfully";
        }
    }
}
