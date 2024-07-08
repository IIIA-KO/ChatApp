using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Chats.CreateChat
{
    public sealed record CreateChatCommand(UserId CreatorId, ChatName ChatName) : ICommand<ChatId>;
}
