using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Chats;

namespace ChatApp.Application.Chats.GetChatMessages
{
    public sealed record GetChatMessagesQuery(ChatId ChatId)
        : IQuery<IReadOnlyList<MessageResponse>>;
}
