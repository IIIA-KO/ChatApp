using ChatApp.Domain.Chats;
using ChatApp.Domain.UnitTests.Chats;
using ChatApp.Domain.UnitTests.Infrastructure;
using ChatApp.Domain.Users;
using FluentAssertions;

namespace ChatApp.Domain.UnitTests.Users
{
    public class UserTests : BaseTest
    {
        [Fact]
        public void Create_Should_SetProperties()
        {
            // Act
            User user = User.Create(UserData.UserName).Value;

            // Assert
            user.UserName.Should().Be(UserData.UserName);
            user.Chats.Should().NotBeNull();
            user.CreatedChats.Should().NotBeNull();
        }

        [Fact]
        public void Create_Should_RaiseCreatedDomainEvent()
        {
            // Act
            User user = User.Create(UserData.UserName).Value;

            // Assert
            UserCreatedDomainEvent userCreatedDomainEvent = AssertDomainEventWasPublished<
                UserCreatedDomainEvent,
                UserId
            >(user);

            userCreatedDomainEvent.UserId.Should().Be(user.Id);
        }

        [Fact]
        public void AddCreatedChat_Should_AddChat()
        {
            // Arrange
            User user = User.Create(UserData.UserName).Value;
            Chat chat = Chat.Create(ChatData.ChatName).Value;

            // Act
            user.AddCreatedChat(chat);

            // Assert
            user.CreatedChats.Should().NotBeNullOrEmpty().And.HaveCount(1);
            user.CreatedChats.Should().Contain(chat);
        }

        [Fact]
        public void AddCreatedChatTwice_ShouldNot_AddChatAgain()
        {
            // Arrange
            User user = User.Create(UserData.UserName).Value;
            Chat chat = Chat.Create(ChatData.ChatName).Value;

            // Act
            user.AddCreatedChat(chat);
            user.AddCreatedChat(chat);

            // Assert
            user.CreatedChats.Should().NotBeNullOrEmpty().And.HaveCount(1);
            user.CreatedChats.Should().Contain(chat);
        }

        [Fact]
        public void AddChat_Should_AddChat()
        {
            // Arrange
            User user = User.Create(UserData.UserName).Value;
            Chat chat = Chat.Create(ChatData.ChatName).Value;

            // Act
            user.AddChat(chat);

            // Assert
            user.Chats.Should().NotBeNullOrEmpty().And.HaveCount(1);
            user.Chats.Should().Contain(chat);
        }

        [Fact]
        public void AddChatTwice_ShouldNot_AddChatAgain()
        {
            // Arrange
            User user = User.Create(UserData.UserName).Value;
            Chat chat = Chat.Create(ChatData.ChatName).Value;

            // Act
            user.AddChat(chat);
            user.AddChat(chat);

            // Assert
            user.Chats.Should().NotBeNullOrEmpty().And.HaveCount(1);
            user.Chats.Should().Contain(chat);
        }

        [Fact]
        public void RemoveChat_Should_RemoveChat()
        {
            // Arrange
            User user = User.Create(UserData.UserName).Value;
            Chat chat = Chat.Create(ChatData.ChatName).Value;

            // Act
            user.AddChat(chat);
            user.RemoveChat(chat);

            // Assert
            user.Chats.Should().BeEmpty();
        }
    }
}
