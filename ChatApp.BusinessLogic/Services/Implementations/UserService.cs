using AutoMapper;
using ChatApp.BusinessLogic.Exceptions;
using ChatApp.BusinessLogic.Services.Interfaces;
using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Repositories.Interfaces;
using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.User;

namespace ChatApp.BusinessLogic.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private IMapper _mapper;

    public UserService(IUserRepository userRepositor, IMapper mapper)
    {
        _userRepository = userRepositor;
        _mapper = mapper;
    }

    public async Task<UserDto> AddAsync(UserRequest user)
    {
        if(await _userRepository.ExistAsync(user.Email, user.UserName))
        {
            throw new ExistException("A user with this email alredy exist");
        }

        var userCreate = _mapper.Map<User>(user);
        userCreate.RoleId = 1;
        var result = await _userRepository.AddAsync(userCreate);

        return _mapper.Map<UserDto>(result);
    }

    public async Task DeleteAsync(int userId)
    {
        var user = await GetOneAsync(userId);
        await _userRepository.DeleteAsync(user);
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        return _mapper.Map<List<UserDto>>(await _userRepository.GetAllAsync());
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        return _mapper.Map<UserDto>(await GetOneAsync(id));
    }

    public async Task<UserDto?> GetUserByUserNameOrEmailAsync(string userNameOrEmail)
    {
        var user = await _userRepository.GetUserByUserNameOrEmailAsync(userNameOrEmail);

        if (user is null)
        {
            throw new NotFoundException("This user does not exist");
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateAsync(int id, UserRequest user)
    {
        var userResult = await GetOneAsync(id);
        _mapper.Map(user, userResult);
        await _userRepository.UpdateAsync(userResult);

        return _mapper.Map<UserDto>(user);
    }

    private async Task<User> GetOneAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if(user is null)
        {
            throw new NotFoundException("This user does not exist");
        }

        return user;
    }
}
