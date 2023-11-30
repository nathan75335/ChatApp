using ChatApp.DataAccess.Entities;

namespace ChatApp.DataAccess.Repositories.Interfaces;

public interface IContactRepository
{
    Task<Contact> AddAsync(Contact Contact);
    Task<Contact> UpdateAsync(Contact Contact);
    Task DeleteAsync(Contact Contact);
    Task<List<Contact>> GetContactByUserIdAsync(int userId);
    Task<Contact> GetByIdAsync(int id);
}
