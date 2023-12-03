namespace ChatApp.Client.Desktop.Models;

public class MessageViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ReceiverId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public bool IsRead {  get; set; }
    public string TagName
    {
        get
        {
            return Title.Substring(0, 2);
        }
    }

    public string Color { get; set; }
   public int MessageCount { get; set; }
}
