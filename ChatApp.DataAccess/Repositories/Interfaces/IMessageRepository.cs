using ChatApp.DataAccess.Entities;

namespace ChatApp.DataAccess.Repositories.Interfaces;

public interface IMessageRepository
{
    Task<Message> AddAsync(Message message);
    Task<Message> UpdateAsync(Message message);
    Task DeleteAsync(Message message);
    Task<Message?> GetByIdAsync(int id);
    Task<List<Message>> GetConversationAsync(int senderId, int receiverId);
    Task<List<Message>> GetRecentConversationAsync(int userId);
    Task<List<Message>> GetUnReadMessageAsync(int userId);
}
