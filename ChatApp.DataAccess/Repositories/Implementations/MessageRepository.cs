using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Repositories.Interfaces;
using ChatApp.Shared.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Repositories.Implementations;

public class MessageRepository : IMessageRepository
{
    private readonly DbSet<Message> _messages;
    private readonly ChatContext _chatContext;

    public MessageRepository(ChatContext chatContext)
    {
        _chatContext = chatContext;
        _messages = chatContext.Set<Message>();
    }

    public async Task<Message> AddAsync(Message message)
    {
        _messages.Add(message);
        await _chatContext.SaveChangesAsync();

        return message;
    }

    public async Task DeleteAsync(Message message)
    {
        _messages.Remove(message);
        await _chatContext.SaveChangesAsync();
    }

    public async Task<Message?> GetByIdAsync(int id)
    {
        return await _messages.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<List<Message>> GetConversationAsync(int senderId, int receiverId)
    {
        return await _messages
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Where(x => x.SenderId.Equals(senderId) && x.ReceiverId.Equals(receiverId))
            .OrderByDescending(x => x.TimeStamp)
            .ToListAsync();
    }

    public async Task<List<Message>> GetRecentConversationAsync(int userId)
    {
        return await _messages
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Where(x => x.SenderId.Equals(userId) || x.ReceiverId.Equals(userId))
            .OrderByDescending(x => x.TimeStamp)
            .ToListAsync();
    }

    public async Task<List<Message>> GetUnReadMessageAsync(int userId)
    {
        return await _messages
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Where(x => x.ReceiverId.Equals(userId) && x.MessageStatus.Equals(MessageStatus.UnRead))
            .OrderBy(x => x.TimeStamp)
            .ToListAsync();
    }

    public async Task<Message> UpdateAsync(Message message)
    {
        _messages.Update(message);
        await _chatContext.SaveChangesAsync();

        return message;
    }
}
