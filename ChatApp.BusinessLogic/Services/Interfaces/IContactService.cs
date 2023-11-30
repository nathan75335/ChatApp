using ChatApp.DataAccess.Entities;
using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.Contact;

namespace ChatApp.BusinessLogic.Services.Interfaces;

public interface IContactService
{
    Task<ContactDto> AddAsync(ContactRequest Contact);
    Task<ContactDto> UpdateAsync(int id, ContactRequest Contact);
    Task DeleteAsync(int id);
    Task<List<ContactDto>> GetContactByUserIdAsync(int userId);
    Task<ContactDto> GetByIdAsync(int id);
}
