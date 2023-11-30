using ChatApp.Shared.DTO_s;

namespace ChatApp.DataAccess.Entities;

public class Message
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public User? Sender { get; set; }
    public int ReceiverId { get; set; }
    public User? Receiver { get; set; }
    public string Text {  get; set; }
    public DateTime TimeStamp { get; set; }
    public MessageStatus? MessageStatus { get; set; }
}
