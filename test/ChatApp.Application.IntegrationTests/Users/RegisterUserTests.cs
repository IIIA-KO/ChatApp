using ChatApp.Application.IntegrationTests.Infrastructure;
using ChatApp.Application.Users.RegisterUser;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Users;
using FluentAssertions;

namespace ChatApp.Application.IntegrationTests.Users
{
    public class RegisterUserTests : BaseIntegrationTest
    {
        public RegisterUserTests(IntegrationTestWebAppFactory factory)
            : base(factory) { }

        [Fact]
        public async Task RegisterUser_Should_ReturnSuccess_When_CredentialValid()
        {
            // Arrange
            var command = new RegisterUserCommand(UserData.UserName);

            // Act
            Result<UserId> result = await this.Sender.Send(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
