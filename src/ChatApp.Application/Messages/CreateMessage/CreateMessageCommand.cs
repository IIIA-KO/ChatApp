using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Messages;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Messages.CreateMessage
{
    public sealed record CreateMessageCommand(UserId CreatorId, ChatId ChatId, Content Content)
        : ICommand;
}
