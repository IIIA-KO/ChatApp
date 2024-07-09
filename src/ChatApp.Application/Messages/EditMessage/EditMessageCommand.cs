using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Messages;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Messages.EditMessage
{
    public sealed record EditMessageCommand(UserId CreatorId, MessageId MessageId, Content Content)
        : ICommand;
}
