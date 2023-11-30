using AutoMapper;
using ChatApp.BusinessLogic.Exceptions;
using ChatApp.BusinessLogic.Services.Interfaces;
using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Repositories.Interfaces;
using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.Message;

namespace ChatApp.BusinessLogic.Services.Implementations;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public MessageService(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<MessageDto> AddAsync(MessageRequest message)
    {
        var result = await _messageRepository.AddAsync(_mapper.Map<Message>(message));

        return _mapper.Map<MessageDto>(result);
    }

    public async Task DeleteAsync(int id)
    {
        var message = await GetOneAsync(id);
        await _messageRepository.DeleteAsync(message);
    }

    public async Task<MessageDto?> GetByIdAsync(int id)
    {
        return  _mapper.Map<MessageDto>(await GetOneAsync(id));
    }

    public async Task<List<MessageDto>> GetConversationAsync(int senderId, int receiverId)
    {
        return _mapper.Map<List<MessageDto>>(await _messageRepository.GetConversationAsync(senderId, receiverId));
    }

    public async Task<List<MessageDto>> GetRecentConversationAsync(int userId)
    {
        return _mapper.Map<List<MessageDto>>(await _messageRepository.GetRecentConversationAsync(userId));
    }

    public async Task<List<MessageDto>> GetUnReadMessageAsync(int userId)
    {
        return _mapper.Map<List<MessageDto>>(await _messageRepository.GetUnReadMessageAsync(userId));
    }

    public async Task<MessageDto> UpdateAsync(int id, MessageRequest message)
    {
        var messageUpdate = await GetOneAsync(id);
        _mapper.Map(messageUpdate, message);
        var result = await _messageRepository.UpdateAsync(messageUpdate);

        return _mapper.Map<MessageDto>(messageUpdate);
    }

    private async Task<Message> GetOneAsync(int id)
    {
        var message = await _messageRepository.GetByIdAsync(id);

        if(message is null)
        {
            throw new NotFoundException("this message does not exist");
        }

        return message;
    }
}
