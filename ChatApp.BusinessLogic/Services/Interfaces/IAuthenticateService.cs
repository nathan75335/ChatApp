using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.User;

namespace ChatApp.BusinessLogic.Services.Interfaces;

public interface IAuthenticateService
{
    Task<TokenDto> LoginAsync(UserLoginRequest userLoginRequest);
}
