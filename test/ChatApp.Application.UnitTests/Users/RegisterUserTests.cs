using ChatApp.Application.Users.RegisterUser;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Users;
using FluentAssertions;
using NSubstitute;

namespace ChatApp.Application.UnitTests.Users
{
    public class RegisterUserTests
    {
        private static readonly RegisterUserCommand Command = new(UserData.UserName);

        private readonly RegisterUserCommandHandler _handler;

        private readonly IUserRepository _userRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public RegisterUserTests()
        {
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new RegisterUserCommandHandler(
                this._userRepositoryMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserExists()
        {
            // Arrange
            this._userRepositoryMock.UserExistsByUsernameAsync(Command.UserName).Returns(true);

            // Act
            Result<UserId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.DuplicateUsername);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenCreationFails()
        {
            // Arrange
            this._userRepositoryMock.UserExistsByUsernameAsync(Command.UserName).Returns(false);

            // Act
            var invalidCommand = new RegisterUserCommand(new UserName(string.Empty));

            Result<UserId> result = await this._handler.Handle(invalidCommand, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }

        [Fact]
        public async Task Handle_Should_SuccessfullyRegisterUser()
        {
            // Arrange
            this._userRepositoryMock.UserExistsByUsernameAsync(Command.UserName).Returns(false);

            // Act
            Result<UserId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
