using AutoMapper;
using ChatApp.DataAccess.Entities;
using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.Contact;
using ChatApp.Shared.Requests.Message;
using ChatApp.Shared.Requests.User;

namespace ChatApp.API.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile() 
        {
            CreateMap<UserRequest, User>()
                .ReverseMap();
            CreateMap<User, UserDto>()
                .ReverseMap();
            CreateMap<MessageRequest, Message>()
                .ReverseMap();
            CreateMap<Message, MessageDto>()
                .ReverseMap();
            CreateMap<ContactRequest, Contact>()
                .ReverseMap();
            CreateMap<Contact, ContactDto>()
                .ReverseMap();
            CreateMap<Role, RoleDto>()
                .ReverseMap();
        }

    }
}
