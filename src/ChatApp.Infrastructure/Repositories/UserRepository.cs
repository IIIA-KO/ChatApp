using ChatApp.Domain.Users;
using ChatApp.Infrastructure.Specifications.Users;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<User?> GetByWithChatsIdAsync(
            UserId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new UserByIdWithChatsSpecification(id))
                .FirstAsync(cancellationToken);
        }

        public async Task<User?> GetByIdWithCreatedChatsAsync(
            UserId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new UserByIdWithCreatedChatsSpecification(id))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> UserExists(
            UserName userName,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<User>()
                .AsNoTracking()
                .AnyAsync(user => user.UserName == userName, cancellationToken);
        }
    }
}
