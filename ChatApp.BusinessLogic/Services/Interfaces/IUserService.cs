using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.User;

namespace ChatApp.BusinessLogic.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> AddAsync(UserRequest user);
    Task<UserDto> UpdateAsync(int id, UserRequest user);
    Task DeleteAsync(int userId);
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> GetUserByUserNameOrEmailAsync(string userNameOrEmail);
    Task<List<UserDto>> GetAllAsync();
}
