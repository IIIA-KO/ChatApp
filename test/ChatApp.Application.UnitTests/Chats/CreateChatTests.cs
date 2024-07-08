using ChatApp.Application.Chats.CreateChat;
using ChatApp.Application.UnitTests.Users;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Users;
using FluentAssertions;
using NSubstitute;

namespace ChatApp.Application.UnitTests.Chats
{
    public class CreateChatTests
    {
        public static readonly User User = UserData.Create();
        public static readonly CreateChatCommand Command = new(User.Id, ChatData.ChatName);

        private readonly IChatRepository _chatRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly CreateChatCommandHandler _handler;

        public CreateChatTests()
        {
            this._chatRepositoryMock = Substitute.For<IChatRepository>();
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new CreateChatCommandHandler(
                this._chatRepositoryMock,
                this._userRepositoryMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserIsNull()
        {
            // Arrange
            this._userRepositoryMock.GetByIdAsync(User.Id, Arg.Any<CancellationToken>())
                .Returns((User?)null);

            // Act
            Result<ChatId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenChatNameCreationFails()
        {
            // Arrange
            this._userRepositoryMock.GetByIdAsync(User.Id, Arg.Any<CancellationToken>())
                .Returns(User);

            var invalidCommand = new CreateChatCommand(User.Id, new ChatName(string.Empty));

            // Act
            Result<ChatId> result = await this._handler.Handle(invalidCommand, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ChatErrors.InvalidName);
        }

        [Fact]
        public async Task Handle_Should_SuccessfullyCreateChat()
        {
            // Arrange
            this._userRepositoryMock.GetByIdAsync(User.Id, Arg.Any<CancellationToken>())
                .Returns(User);

            // Act
            Result<ChatId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
