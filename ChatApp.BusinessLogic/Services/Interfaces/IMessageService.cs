using ChatApp.DataAccess.Entities;
using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.Message;

namespace ChatApp.BusinessLogic.Services.Interfaces;

public interface IMessageService
{
    Task<MessageDto> AddAsync(MessageRequest message);
    Task<MessageDto> UpdateAsync(int id, MessageRequest message);
    Task DeleteAsync(int id);
    Task<MessageDto?> GetByIdAsync(int id);
    Task<List<MessageDto>> GetConversationAsync(int senderId, int receiverId);
    Task<List<MessageDto>> GetRecentConversationAsync(int userId);
    Task<List<MessageDto>> GetUnReadMessageAsync(int userId);
    Task<List<MessageDto>> GetAllAsync();
}
