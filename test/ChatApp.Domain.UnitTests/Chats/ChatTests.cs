using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Messages;
using ChatApp.Domain.UnitTests.Messages;
using ChatApp.Domain.UnitTests.Users;
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
            Chat chat = Chat.Create(ChatData.ChatName).Value;

            // Assert
            chat.ChatName.Should().Be(ChatData.ChatName);
            chat.Participants.Should().NotBeNull();
            chat.Messages.Should().NotBeNull();
        }

        [Fact]
        public void Create_Should_ReturnFailure_WhenChatNameIsNull()
        {
            // Act
            Result<Chat> result = Chat.Create(null!);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ChatErrors.InvalidName);
        }

        [Fact]
        public void AddParticipant_Should_AddParticipant()
        {
            // Arrange
            Chat chat = Chat.Create(ChatData.ChatName).Value;
            User participant = User.Create(UserData.UserName).Value;

            // Act
            chat.AddParticipant(participant);

            // Assert
            chat.Participants.Should().NotBeNullOrEmpty().And.HaveCount(1);
            chat.Participants.Should().Contain(participant);
        }

        [Fact]
        public void AddParticipantTwice_ShouldNot_AddParticipantAgain()
        {
            // Arrange
            Chat chat = Chat.Create(ChatData.ChatName).Value;
            User participant = User.Create(UserData.UserName).Value;

            // Act
            chat.AddParticipant(participant);
            chat.AddParticipant(participant);

            // Assert
            chat.Participants.Should().NotBeNullOrEmpty().And.HaveCount(1);
            chat.Participants.Should().Contain(participant);
        }

        [Fact]
        public void AddMessage_Should_AddMessage()
        {
            // Arrange
            Chat chat = Chat.Create(ChatData.ChatName).Value;
            Message message = Message.Create(MessageData.Content).Value;

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
            Chat chat = Chat.Create(ChatData.ChatName).Value;
            Message message = Message.Create(MessageData.Content).Value;

            // Act
            chat.AddMessage(message);
            chat.AddMessage(message);

            // Assert
            chat.Messages.Should().NotBeNullOrEmpty().And.HaveCount(1);
            chat.Messages.Should().Contain(message);
        }

        [Fact]
        public void SetCreator_Should_SetCreatorAndCreatorId()
        {
            // Arrange
            Chat chat = Chat.Create(ChatData.ChatName).Value;
            User creator = User.Create(UserData.UserName).Value;

            // Act
            chat.SetCreator(creator);

            // Assert
            chat.Creator.Should().Be(creator);
            chat.CreatorId.Should().Be(creator.Id);
        }

        [Fact]
        public void SetCreator_Should_Throw_WhenCreatorIsNull()
        {
            // Arrange
            Chat chat = Chat.Create(ChatData.ChatName).Value;

            // Act
            Action action = () => chat.SetCreator(null!);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }
    }
}
