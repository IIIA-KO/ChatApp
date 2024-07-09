using ChatApp.Domain.Users;

namespace ChatApp.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<bool> UserExistsByIdAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ExistsAsync(user => user.Id == userId, cancellationToken);
        }

        public async Task<bool> UserExistsByUsernameAsync(
            UserName userName,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ExistsAsync(user => user.UserName == userName, cancellationToken);
        }
    }
}
