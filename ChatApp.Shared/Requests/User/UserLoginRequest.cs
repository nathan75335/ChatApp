namespace ChatApp.Shared.Requests.User;

public class UserLoginRequest
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }
}
