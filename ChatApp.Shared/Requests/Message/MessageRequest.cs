using ChatApp.Shared.DTO_s;

namespace ChatApp.Shared.Requests.Message;

public class MessageRequest
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Text { get; set; }
    public DateTime TimeStamp { get; set; }
    public MessageStatus? MessageStatus { get; set; }
}
