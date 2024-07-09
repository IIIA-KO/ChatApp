using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Users;

namespace ChatApp.Domain.Messages
{
    public sealed class Message : Entity<MessageId>
    {
        private Message(
            MessageId id,
            Content content,
            ChatId chatId,
            UserId userId,
            DateTime sentAt
        )
            : base(id)
        {
            this.Content = content;
            this.ChatId = chatId;
            this.UserId = userId;
            this.SentAt = sentAt;
        }

        private Message() { }

        public Content Content { get; set; }

        public DateTime SentAt { get; init; }

        public UserId UserId { get; init; }

        public User User { get; init; }

        public ChatId ChatId { get; init; }

        public Chat Chat { get; init; }

        public static Result<Message> Create(Content content, ChatId chatId, UserId userId)
        {
            if (content is null || string.IsNullOrEmpty(content.Value))
            {
                return Result.Failure<Message>(MessageErrors.InvalidContent);
            }

            return new Message(MessageId.New(), content, chatId, userId, DateTime.UtcNow);
        }
    }
}
