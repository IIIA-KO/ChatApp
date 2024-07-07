using ChatApp.Domain.Messages;
using ChatApp.Infrastructure.Specifications.Messages;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories
{
    internal sealed class MessageRepository : Repository<Message, MessageId>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<Message?> GetByIdWithChatAsync(
            MessageId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new MessageByIdWithChatSpecification(id))
                .FirstAsync(cancellationToken);
        }

        public async Task<Message?> GetByIdAWithUsersync(
            MessageId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new MessageByIdWithUserSpecification(id))
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
