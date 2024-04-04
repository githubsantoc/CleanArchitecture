using Application.Command.CustomerCommand;
using Application.Command.CustomerCommand.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("Customer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(command, cancellationToken);
                return response == null ? throw new Exception("Response is null") : (IActionResult)Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GetCustomers")]
        public async Task<IActionResult> ReadCustomer(CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetCustomerQuery();
                var response = await _mediator.Send(query, cancellationToken);
                return response == null ? throw new Exception("Response is null") : (IActionResult)Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(501, $"Not Implemented : {e.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(command, cancellationToken);
                return response == null ? throw new Exception("Response is null") : (IActionResult)Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
