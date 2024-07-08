namespace ChatApp.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

        Task<User?> GetByWithChatsIdAsync(UserId id, CancellationToken cancellationToken = default);

        Task<User?> GetByIdWithCreatedChatsAsync(
            UserId id,
            CancellationToken cancellationToken = default
        );

        Task<bool> UserExistsByUsernameAsync(
            UserName userName,
            CancellationToken cancellationToken = default
        );

        void Add(User user);
    }
}
