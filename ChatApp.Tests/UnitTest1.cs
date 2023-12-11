using AutoMapper;
using ChatApp.BusinessLogic.Exceptions;
using ChatApp.BusinessLogic.Services.Implementations;
using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Repositories.Interfaces;
using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.Message;
using Moq;

namespace ChatApp.Tests;

public class Tests
{
    private readonly Mock<IMessageRepository> _mockMessageRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly MessageService _messageService;

    public Tests()
    {
        _mockMessageRepository = new Mock<IMessageRepository>();
        _mockMapper = new Mock<IMapper>();
        _messageService = new MessageService(_mockMessageRepository.Object, _mockMapper.Object);
    }

    [Test]
    public async Task AddAsync_ValidMessageRequest_ReturnsMessageDto()
    {
        // Arrange
        var messageRequest = new MessageRequest { Text = "Hello", SenderId = 1, ReceiverId = 2 };
        var messageEntity = new Message { Text = "Hello", SenderId = 1, ReceiverId = 2, TimeStamp = DateTime.Now };
        var messageDto = new MessageDto { Text = "Hello", Sender = new UserDto(), Receiver = new UserDto(), TimeStamp = DateTime.Now };

        _mockMapper.Setup(m => m.Map<Message>(messageRequest)).Returns(messageEntity);
        _mockMessageRepository.Setup(repo => repo.AddAsync(messageEntity)).ReturnsAsync(messageEntity);
        _mockMapper.Setup(m => m.Map<MessageDto>(messageEntity)).Returns(messageDto);

        // Act
        var result = await _messageService.AddAsync(messageRequest);

        // Assert
        Assert.AreEqual(messageDto, result);
    }

    [Test]
    public async Task DeleteAsync_ExistingId_CallsRepositoryDeleteAsync()
    {
        // Arrange
        var messageId = 1;
        var message = new Message { Id = messageId };

        _mockMessageRepository.Setup(repo => repo.GetByIdAsync(messageId)).ReturnsAsync(message);

        // Act
        await _messageService.DeleteAsync(messageId);

        // Assert
        _mockMessageRepository.Verify(repo => repo.DeleteAsync(message), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_ReturnsListOfMessageDto()
    {
        // Arrange
        var messages = new List<Message>
            {
                new Message { Id = 1, Text = "Message 1", SenderId = 1, ReceiverId = 2, TimeStamp = DateTime.Now },
                new Message { Id = 2, Text = "Message 2", SenderId = 2, ReceiverId = 1, TimeStamp = DateTime.Now }
            };
        var messageDtos = new List<MessageDto>
            {
                new MessageDto { Id = 1, Text = "Message 1", Sender = new UserDto(), Receiver = new UserDto(), TimeStamp = DateTime.Now },
                new MessageDto { Id = 2, Text = "Message 2", Sender = new UserDto(), Receiver = new UserDto(), TimeStamp = DateTime.Now }
            };

        _mockMessageRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(messages);
        _mockMapper.Setup(m => m.Map<List<MessageDto>>(messages)).Returns(messageDtos);

        // Act
        var result = await _messageService.GetAllAsync();

        // Assert
        Assert.AreEqual(messageDtos, result);
    }

    [Test]
    public async Task UpdateAsync_NonexistentId_ThrowsNotFoundException()
    {
        // Arrange
        var messageId = 999;
        var messageRequest = new MessageRequest { Text = "Updated Message" };

        _mockMessageRepository.Setup(repo => repo.GetByIdAsync(messageId)).ReturnsAsync((Message)null);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(async() =>
        {
            await _messageService.UpdateAsync(messageId, messageRequest);
        });
    }

}