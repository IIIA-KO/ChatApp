using ChatApp.Domain.Chats;
using ChatApp.Domain.UserChats;
using ChatApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories
{
    internal sealed class UserChatRepository : IUserChatRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserChatRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void AddUserToChat(UserChat userChat)
        {
            this._dbContext.Set<UserChat>().Add(userChat);
        }

        public void RemoveUserFromChat(UserChat userChat)
        {
            this._dbContext.Set<UserChat>().Remove(userChat);
        }

        public async Task<List<Chat>> GetChatsAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                ._dbContext.Set<UserChat>()
                .Where(uc => uc.UserId == userId)
                .Include(uc => uc.Chat)
                .Select(uc => uc.Chat)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<User>> GetUsersAsync(
            ChatId chatId,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                ._dbContext.Set<UserChat>()
                .Where(uc => uc.ChatId == chatId)
                .Include(uc => uc.User)
                .Select(uc => uc.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsUserConnectedToChat(
            UserId userId,
            ChatId chatId,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                ._dbContext.Set<UserChat>()
                .AnyAsync(uc => uc.UserId == userId && uc.ChatId == chatId, cancellationToken);
        }
    }
}
