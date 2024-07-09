using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Chats.DeleteChat
{
    public sealed record DeleteChatCommand(UserId UserId, ChatId ChatId) : ICommand;
}
