using Application.UsersCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromQuery] CreateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _mediator.Send(command, cancellationToken);
                return data == null ? throw new Exception("null response") : (IActionResult)Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser([FromQuery] CreateLoginQuery u1, CancellationToken cancellationToken)
        {
            try
            {
                var data1 = await _mediator.Send(u1, cancellationToken);
                return data1 == null ? throw new Exception("null response") : (IActionResult)Ok(data1);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
