namespace ChatApp.Shared.DTO_s;

public class ContactDto
{
    public int Id { get; set; }
    public UserDto User { get; set; }
    public UserDto ContactUser {  get; set; }   
}
