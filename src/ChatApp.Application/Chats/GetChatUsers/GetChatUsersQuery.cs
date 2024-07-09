using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Chats;

namespace ChatApp.Application.Chats.GetChatUsers
{
    public sealed record GetChatUsersQuery(ChatId ChatId) : IQuery<IReadOnlyList<UserResponse>>;
}
