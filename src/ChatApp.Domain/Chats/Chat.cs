using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Messages;
using ChatApp.Domain.Users;

namespace ChatApp.Domain.Chats
{
    public sealed class Chat : Entity<ChatId>
    {
        private readonly List<Message> _messages = [];

        private Chat(ChatId chatId, ChatName chatName, UserId creatorId)
            : base(chatId)
        {
            this.ChatName = chatName;
            this.CreatorId = creatorId;
        }

        private Chat() { }

        public ChatName ChatName { get; private set; }

        public UserId CreatorId { get; private set; }

        public User Creator { get; }

        public IReadOnlyCollection<Message> Messages => this._messages.AsReadOnly();

        public void AddMessage(Message message)
        {
            if (!this._messages.Contains(message))
            {
                this._messages.Add(message);
            }
        }

        public void RemoveMessage(Message message)
        {
            this._messages.Remove(message);
        }

        public static Result<Chat> Create(ChatName chatName, UserId creatorId)
        {
            if (chatName is null || string.IsNullOrEmpty(chatName.Value))
            {
                return Result.Failure<Chat>(ChatErrors.InvalidName);
            }

            return new Chat(ChatId.New(), chatName, creatorId);
        }
    }
}
