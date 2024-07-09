using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Messages;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Messages.DeleteMessage
{
    public sealed record DeleteMessageCommand(UserId CreatorId, MessageId MessageId) : ICommand;
}
