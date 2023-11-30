namespace ChatApp.Shared.DTO_s;

public class TokenDto
{
    public string Token { get; set; }
    public DateTime ExpirationTime { get; set; }
    public UserDto User { get; set; }
}
