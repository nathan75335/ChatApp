using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly ChatContext _chatContext;

    public UserRepository(ChatContext chatContext)
    {
        _users = chatContext.Set<User>();
        _chatContext = chatContext;
    }
    public async Task<User> AddAsync(User user)
    {
        _users.Add(user);
        await _chatContext.SaveChangesAsync();

        return user;
    }

    public async Task<User?> CheckPasswordAsync(string userNameOrEmail, string password)
    {
        return await _users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email.Equals(userNameOrEmail) || x.UserName.Equals(userNameOrEmail) && x.Password.Equals(password));
    }

    public async Task DeleteAsync(User user)
    {
        _users.Remove(user);    
        await _chatContext.SaveChangesAsync();
    }

    public async Task<bool> ExistAsync(string email, string username)
    {
        return await _users
            .Include(x => x.Role)
            .AnyAsync(x => x.Email.Equals(email) || x.UserName.Equals(username));
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _users.Include(x => x.Role).ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetUserByUserNameOrEmailAsync(string userNameOrEmail)
    {
        return await _users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email.Equals(userNameOrEmail) || x.UserName.Equals(userNameOrEmail));
    }

    public async Task<User> UpdateAsync(User user)
    {
        _users.Update(user);
        await _chatContext.SaveChangesAsync();

        return user;
    }
}
