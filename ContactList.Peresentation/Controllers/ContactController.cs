using ContactList.Application.Features.Contacts.Command.CreateContact;
using ContactList.Application.Features.Contacts.Command.DeleteContact;
using ContactList.Application.Features.Contacts.Command.UpdateContact;
using ContactList.Application.Features.Contacts.Queries.GetContactById;
using ContactList.Application.Features.Contacts.Queries.GettAllContacts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactList.Peresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactCommand command)
        {
            if (command == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            var creatorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (creatorId == null)
            {
                return Unauthorized("Invalid User request");
            }
            command.UserId = Guid.Parse(creatorId);
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GettAllContacts()
        {
            var creatorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (creatorId == null)
            {
                return Unauthorized("Invalid User request,Please try to login");
            }
            var result = await _mediator.Send(new GetAllContactsQuery(creatorId));
            if (result.Success)
            {
                return Ok(result);
            }
            else if (result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { message = result.ErrorMessage });
            }
            else if (result.ErrorMessage.Contains("validation", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = result.ErrorMessage });
            }
            else
            {
                return StatusCode(500, new { message = result.ErrorMessage });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var creatorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (creatorId == null)
            {
                return Unauthorized("Invalid User request,Please try to login");
            }
            var result = await _mediator.Send(new GetContactByIdQuery(id, creatorId));
            if (result.Success)
            {
                if (result.Data == null)
                {
                    return NotFound("Contact not found");
                }
                return Ok(result);
            }
            else if (result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { message = result.ErrorMessage });
            }
            else
            {
                return StatusCode(500, new { message = result.ErrorMessage });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(Guid id, [FromBody] UpdateContactCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Task Id mismatch");
            }
            var creatorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (creatorId == null)
            {
                return Unauthorized("Invalid User request,Please try to login");
            }
            command.creatorId = creatorId;
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return NoContent();
            }
            else if (result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { message = result.ErrorMessage });
            }
            else
            {
                return StatusCode(500, new { message = result.ErrorMessage });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var creatorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (creatorId == null)
            {
                return Unauthorized("Invalid User request,Please try to login");
            }
            var result = await _mediator.Send(new DeleteContactCommand(id, creatorId));
            if (result.Success)
            {
                return NoContent();
            }
            else if (result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { message = result.ErrorMessage });
            }
            else
            {
                return StatusCode(500, new { message = result.ErrorMessage });
            }
        }
    }
}
