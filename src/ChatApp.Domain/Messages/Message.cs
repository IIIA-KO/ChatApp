using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Users;

namespace ChatApp.Domain.Messages
{
    public sealed class Message : Entity<MessageId>
    {
        private Message(MessageId id, Content content, DateTime sentAt)
            : base(id)
        {
            this.Content = content;
            this.SentAt = sentAt;
        }

        private Message() { }

        public Content Content { get; private set; }

        public DateTime SentAt { get; private set; }

        public UserId UserId { get; private set; }

        public User User { get; private set; }

        public ChatId ChatId { get; private set; }

        public Chat Chat { get; private set; }

        public static Result<Message> Create(Content content)
        {
            if (content is null || string.IsNullOrEmpty(content.Value))
            {
                return Result.Failure<Message>(MessageErrors.InvalidContent);
            }

            return new Message(MessageId.New(), content, DateTime.UtcNow);
        }

        public void SetUser(User user)
        {
            this.User =
                user ?? throw new ArgumentNullException(nameof(user), "User cannot be null");

            this.UserId = user.Id;
        }

        public void SetChat(Chat chat)
        {
            this.Chat =
                chat ?? throw new ArgumentNullException(nameof(chat), "Chat cannot be null");

            this.ChatId = chat.Id;
        }
    }
}
