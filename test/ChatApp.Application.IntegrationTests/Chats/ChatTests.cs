using ChatApp.Application.Chats.CreateChat;
using ChatApp.Application.IntegrationTests.Infrastructure;
using ChatApp.Application.UnitTests.Chats;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Users;
using FluentAssertions;

namespace ChatApp.Application.IntegrationTests.Chats
{
    public class ChatTests : BaseIntegrationTest
    {
        private static readonly UserId UserId = UserId.New();

        public ChatTests(IntegrationTestWebAppFactory factory)
            : base(factory) { }

        [Fact]
        public async Task CreateChat_Should_ReturnFailure_WhenUserIsNotFound()
        {
            // Arrange
            var command = new CreateChatCommand(UserId, ChatData.ChatName);

            // Act
            Result<Domain.Chats.ChatId> result = await this.Sender.Send(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotFound);
        }
    }
}
