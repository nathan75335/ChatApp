using ChatApp.BusinessLogic.Services.Implementations;
using ChatApp.BusinessLogic.Services.Interfaces;
using ChatApp.Shared.Requests.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers;

[ApiController, Route("users"), Authorize]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessagesController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost("messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(MessageRequest messageRequest)
    {
        return Ok(await _messageService.AddAsync(messageRequest));
    }

    [HttpPut("messages/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, MessageRequest messageRequest)
    {
        return Ok(await _messageService.UpdateAsync(id, messageRequest));
    }

    [HttpDelete("messages/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        await _messageService.DeleteAsync(id);

        return NoContent();
    }

    [HttpGet("{userId:int}/messages/new")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUnreadMessages(int userId)
    { 
        return Ok(await _messageService.GetUnReadMessageAsync(userId));
    }

    [HttpGet("{userId:int}/messages/recent-conversation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRecentConversations(int userId)
    {
        return Ok(await _messageService.GetRecentConversationAsync(userId));
    }

    [HttpGet("{senderId:int}/messages/{receiverId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetConversation(int senderId, int receiverId)
    {
        return Ok(await _messageService.GetConversationAsync(senderId, receiverId));
    }

    [HttpGet("messages/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _messageService.GetByIdAsync(id));
    }

}
