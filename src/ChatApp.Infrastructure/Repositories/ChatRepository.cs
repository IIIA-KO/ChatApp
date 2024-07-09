using ChatApp.Domain.Chats;
using ChatApp.Infrastructure.Specifications.Chats;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories
{
    internal sealed class ChatRepository : Repository<Chat, ChatId>, IChatRepository
    {
        public ChatRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<Chat?> GetByIdWithCreatorAsync(
            ChatId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new ChatByIdWithCreator(id))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Chat?> GetByIdWithMessagesAsync(
            ChatId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new ChatByIdWithMessagesSpecification(id))
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
