using ChatApp.BusinessLogic.Services.Interfaces;
using ChatApp.Shared.Requests.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers;

[ApiController, Route("users"), Authorize]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost("contacts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    private async Task<IActionResult> Add(ContactRequest contactRequest)
    {
        return Ok(await _contactService.AddAsync(contactRequest));
    }

    [HttpDelete("contacts/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        await _contactService.DeleteAsync(id);

        return NoContent();
    }

    [HttpPut("contacts/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, ContactRequest contactRequest)
    {
        return Ok(await _contactService.UpdateAsync(id, contactRequest));
    }

    [HttpGet("{userId:int}/contacts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        return Ok(await _contactService.GetContactByUserIdAsync(userId));
    }

    [HttpGet("contacts/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _contactService.GetByIdAsync(id));
    }
}
