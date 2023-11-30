using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Repositories.Implementations;

public class ContactRepostiory : IContactRepository
{
    private readonly DbSet<Contact> _contacts;
    private readonly ChatContext _chatContext;

    public ContactRepostiory(ChatContext chatContext)
    {
        _chatContext = chatContext;
        _contacts =  _chatContext.Set<Contact>();
    }

    public async Task<Contact> AddAsync(Contact Contact)
    {
        _contacts.Add(Contact);
        await _chatContext.SaveChangesAsync();

        return Contact;
    }

    public async Task DeleteAsync(Contact Contact)
    {
        _contacts.Remove(Contact);
        await _chatContext.SaveChangesAsync();
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _contacts
            .Include(x => x.UserId)
            .Include(x => x.ContactUserId)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<List<Contact>> GetContactByUserIdAsync(int userId)
    {
        return await _contacts
            .Include(x => x.UserId)
            .Include(x => x.ContactUserId)
            .Where(x => x.UserId.Equals(userId))
            .ToListAsync();
    }

    public async Task<Contact> UpdateAsync(Contact Contact)
    {
        _contacts.Update(Contact);
        await _chatContext.SaveChangesAsync();

        return Contact;
    }

}
