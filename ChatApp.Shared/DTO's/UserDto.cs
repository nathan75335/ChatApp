namespace ChatApp.Shared.DTO_s;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public RoleDto Role { get; set; } 

}
