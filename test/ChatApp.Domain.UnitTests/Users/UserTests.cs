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
    }
}
