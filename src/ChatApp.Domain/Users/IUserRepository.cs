namespace ChatApp.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

        Task<bool> UserExistsByIdAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        );

        Task<bool> UserExistsByUsernameAsync(
            UserName userName,
            CancellationToken cancellationToken = default
        );

        void Add(User user);
    }
}
