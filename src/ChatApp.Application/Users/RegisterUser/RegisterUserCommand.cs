using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Users.RegisterUser
{
    public sealed record RegisterUserCommand(UserName UserName) : ICommand<UserId>;
}
