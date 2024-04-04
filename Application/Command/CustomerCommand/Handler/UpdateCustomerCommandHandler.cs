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
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, string>
    {
        private readonly ICustomerRepo _customer;

        public UpdateCustomerCommandHandler(ICustomerRepo customer)
        {
            _customer = customer;
        }
        public async Task<string> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var user = await _customer.FirstOrDefaultAsy(request.CustomerId);
            if (user == null)
            {
                return "there is no customer of the provided id";
            }
            user.Name = request.Name;
            user.Address = request.Address;
            user.Active = request.Active;
            
            await _customer.UpdateAsy(user);
            return "updated successfully";
        }
    }
}
