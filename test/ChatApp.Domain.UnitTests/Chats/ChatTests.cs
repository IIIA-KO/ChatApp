using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Messages;
using ChatApp.Domain.UnitTests.Messages;
using ChatApp.Domain.Users;
using FluentAssertions;

namespace ChatApp.Domain.UnitTests.Chats
{
    public class ChatTests
    {
        [Fact]
        public void Create_Should_SetProperties()
        {
            // Act
            Chat chat = Chat.Create(ChatData.ChatName, new(Guid.NewGuid())).Value;

            // Assert
            chat.ChatName.Should().Be(ChatData.ChatName);
            chat.Messages.Should().NotBeNull();
        }

        [Fact]
        public void Create_Should_ReturnFailure_WhenChatNameIsNull()
        {
            // Act
            Result<Chat> result = Chat.Create(null!, new(Guid.NewGuid()));

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ChatErrors.InvalidName);
        }

        [Fact]
        public void AddMessage_Should_AddMessage()
        {
            // Arrange
            Chat chat = Chat.Create(ChatData.ChatName, UserId.New()).Value;
            Message message = Message.Create(MessageData.Content, ChatId.New(), UserId.New()).Value;

            // Act
            chat.AddMessage(message);

            // Assert
            chat.Messages.Should().NotBeNullOrEmpty().And.HaveCount(1);
            chat.Messages.Should().Contain(message);
        }

        [Fact]
        public void AddMessageTwice_ShouldNot_AddMessageAgain()
        {
            // Arrange
            Chat chat = Chat.Create(ChatData.ChatName, UserId.New()).Value;
            Message message = Message.Create(MessageData.Content, ChatId.New(), UserId.New()).Value;

            // Act
            chat.AddMessage(message);
            chat.AddMessage(message);

            // Assert
            chat.Messages.Should().NotBeNullOrEmpty().And.HaveCount(1);
            chat.Messages.Should().Contain(message);
        }
    }
}
