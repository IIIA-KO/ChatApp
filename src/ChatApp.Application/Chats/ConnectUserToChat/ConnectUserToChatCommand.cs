using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Chats.ConnectUserToChat
{
    public sealed record ConnectUserToChatCommand(UserId UserId, ChatId ChatId) : ICommand;
}
