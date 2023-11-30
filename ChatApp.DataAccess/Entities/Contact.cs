namespace ChatApp.DataAccess.Entities;

public class Contact
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    public int? ContactUserId { get; set; }
    public User? ContactUser { get; set; }
}
