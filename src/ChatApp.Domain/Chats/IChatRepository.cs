namespace ChatApp.Domain.Chats
{
    public interface IChatRepository
    {
        Task<Chat?> GetByIdAsync(ChatId id, CancellationToken cancellationToken = default);

        Task<Chat?> GetByIdWithMessagesAsync(
            ChatId id,
            CancellationToken cancellationToken = default
        );

        Task<Chat?> GetByIdWithCreatorAsync(
            ChatId id,
            CancellationToken cancellationToken = default
        );

        void Add(Chat chat);
        void Remove(Chat chat);
    }
}
