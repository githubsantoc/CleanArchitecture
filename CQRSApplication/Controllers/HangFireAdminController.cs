
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading;
using Application.Command.HangfireCommand;

namespace CQRSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangFireAdminController : Controller
    {
        private readonly IMediator _imediator;
        public HangFireAdminController(IMediator mediator)
        {
            _imediator = mediator;
        }

        [HttpPost]
        [Route("api/HangFire")]
        public async Task<IActionResult> GenerateHangfire([FromQuery] HangFireCommand request, CancellationToken cancellation)
        {
            try
            {
                var response = await _imediator.Send(request, cancellation);
                return response == null ? throw new Exception("Response is null"): (IActionResult)Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }
    }
}
