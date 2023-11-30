namespace ChatApp.DataAccess.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public List<Message> MessagesSenders { get; set; }
    public List<Message> MessagesReceivers { get; set; }
    public List<Contact> Contacts { get; set; }
    public List<Contact> ContactsUsers { get; set; }
    public Role? Role {  get; set; }
}
