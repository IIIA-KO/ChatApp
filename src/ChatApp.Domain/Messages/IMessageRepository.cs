namespace ChatApp.Domain.Messages
{
    public interface IMessageRepository
    {
        Task<Message?> GetByIdAsync(MessageId id, CancellationToken cancellationToken = default);

        Task<Message?> GetByIdAWithUsersync(
            MessageId id,
            CancellationToken cancellationToken = default
        );

        Task<Message?> GetByIdWithChatAsync(
            MessageId id,
            CancellationToken cancellationToken = default
        );

        void Add(Message message);

        void Remove(Message message);
    }
}
