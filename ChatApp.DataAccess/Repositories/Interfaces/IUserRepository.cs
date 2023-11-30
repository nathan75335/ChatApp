using ChatApp.DataAccess.Entities;

namespace ChatApp.DataAccess.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUserNameOrEmailAsync(string userNameOrEmail);
    Task<bool> ExistAsync(string email, string username);
    Task<List<User>> GetAllAsync();
    Task<User> CheckPasswordAsync(string userNameOrEmail, string password);
}
