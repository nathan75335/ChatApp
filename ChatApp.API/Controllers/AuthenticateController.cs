using ChatApp.BusinessLogic.Services.Interfaces;
using ChatApp.Shared.Requests.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers;

[ApiController, Route("auth")]
public class AuthenticateController : ControllerBase
{
    private readonly IAuthenticateService _autenticateService;

    public AuthenticateController(IAuthenticateService autenticateService)
    {
        _autenticateService = autenticateService;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        return Ok(await _autenticateService.LoginAsync(request));
    }
}
