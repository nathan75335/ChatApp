using AutoMapper;
using ChatApp.BusinessLogic.Configurations;
using ChatApp.BusinessLogic.Exceptions;
using ChatApp.BusinessLogic.Services.Interfaces;
using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Repositories.Interfaces;
using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.User;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ChatApp.BusinessLogic.Services.Implementations;

public class AuthenticateService : IAuthenticateService
{
    private readonly IUserRepository _userRepository;
    private JwtSettings _settings;
    private readonly IMapper _mapper;

    public AuthenticateService(IUserRepository userRepository, IOptions<JwtSettings> settings, IMapper mapper)
    {
        _userRepository = userRepository;
        _settings = settings.Value;
        _mapper = mapper;
    }

    public async Task<TokenDto> LoginAsync(UserLoginRequest userLoginRequest)
    {
        var user = await _userRepository.CheckPasswordAsync(userLoginRequest.UserNameOrEmail, userLoginRequest.Password);

        if(user is null)
        {
            throw new UnAuthorizedException("Check your email or your password");
        }

        var token = GenerateTokenAsync(user);

        return new TokenDto
        {
            Token = token,
            ExpirationTime = DateTime.Now.AddMinutes(10),
            User = _mapper.Map<UserDto>(user),
        };
    }

    /// <summary>
    /// Function To generate the token from the claims of the user
    /// </summary>
    /// <param name="user">The user</param>
    /// <exception cref="NotFoundException">When the user is not found</exception>
    /// <returns>A token as a <see cref="string"/> </returns>
    private string GenerateTokenAsync(User user)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.UTF8.GetBytes(_settings.Key);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name),
            }),

            Expires = DateTime.UtcNow.AddHours(_settings.LifeTime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature),
            Audience = _settings.Audience,
            Issuer = _settings.Issuer
        };

        try
        {
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            throw new NotFoundException("The token can not created " + ex.Message);
        }
    }
}
