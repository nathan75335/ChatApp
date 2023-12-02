namespace ChatApp.Shared.DTO_s
{
    public class MessageDto
    {
        public int Id { get; set; }
        public UserDto Sender { get; set; }
        public UserDto Receiver { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }
        public DateTime TimeStamp { get; set; }
        public MessageStatus? MessageStatus { get; set; }
    }
}
