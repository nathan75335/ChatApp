using AutoMapper;
using ChatApp.BusinessLogic.Exceptions;
using ChatApp.BusinessLogic.Services.Interfaces;
using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Repositories.Interfaces;
using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.Contact;

namespace ChatApp.BusinessLogic.Services.Implementations;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private IUserService _userService;
    private IMapper _mapper;

    public ContactService(IContactRepository contactRepository, IMapper mapper, IUserService userService)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<ContactDto> AddAsync(ContactRequest contact)
    {
        var user = await _userService.GetUserByUserNameOrEmailAsync(contact.Username);

        var contactCreate = new Contact
        {
            UserId = contact.UserId,
            ContactUserId = user.Id
        };

        var result = await _contactRepository.AddAsync(contactCreate);
        
        return _mapper.Map<ContactDto>(result);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await GetOneAsync(id);
        await _contactRepository.DeleteAsync(user);
    }

    public async Task<ContactDto> GetByIdAsync(int id)
    {
        var contact = await GetOneAsync(id);

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<List<ContactDto>> GetContactByUserIdAsync(int userId)
    {
        return  _mapper.Map<List<ContactDto>>(await _contactRepository.GetContactByUserIdAsync(userId));
    }

    public async Task<ContactDto> UpdateAsync(int id, ContactRequest contact)
    {
        var contactUpdate = await GetOneAsync(id);
        var user = _userService.GetUserByUserNameOrEmailAsync(contact.Username);
        contactUpdate.ContactUserId = user.Id;
        var resutl = await _contactRepository.AddAsync(contactUpdate);

        return _mapper.Map<ContactDto>(resutl);
    }

    public async Task<Contact> GetOneAsync(int id)
    {
        var user = await _contactRepository.GetByIdAsync(id);

        if(user is null)
        {
            throw new NotFoundException("This contact does not exist");
        }

        return user;
    }
}
