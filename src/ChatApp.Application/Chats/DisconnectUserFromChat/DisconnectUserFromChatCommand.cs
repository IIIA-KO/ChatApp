using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Chats.DisconnectUserFromChat
{
    public sealed record DisconnectUserFromChatCommand(UserId UserId, ChatId ChatId) : ICommand;
}
