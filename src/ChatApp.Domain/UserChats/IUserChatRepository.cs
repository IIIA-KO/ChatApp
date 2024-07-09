using ChatApp.Domain.Chats;
using ChatApp.Domain.Users;

namespace ChatApp.Domain.UserChats
{
    public interface IUserChatRepository
    {
        Task<List<User>> GetUsersAsync(
            ChatId chatId,
            CancellationToken cancellationToken = default
        );

        Task<List<Chat>> GetChatsAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        );

        void AddUserToChat(UserChat userChat);

        void RemoveUserFromChat(UserChat userChat);

        Task<bool> IsUserConnectedToChat(
            UserId userId,
            ChatId chatId,
            CancellationToken cancellationToken = default
        );
    }
}
