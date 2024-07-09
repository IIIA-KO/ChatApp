using ChatApp.Application.Abstraction.Messaging;

namespace ChatApp.Application.Chats.GetAllChats
{
    public sealed record GetAllChatsQuery : IQuery<IReadOnlyList<ChatResponse>>;
}
