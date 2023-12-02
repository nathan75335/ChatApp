using ChatApp.Shared.DTO_s;

namespace ChatApp.Shared.Requests.Message;

public class MessageRequest
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Text { get; set; }
    public string IsRead { get; set; }
    public MessageStatus? MessageStatus { get; set; }
}
