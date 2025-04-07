using ContactList.Application.Features.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactList.Peresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);
        }
    }
}
