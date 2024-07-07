using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Messages;
using ChatApp.Domain.Users;

namespace ChatApp.Domain.Chats
{
    public sealed class Chat : Entity<ChatId>
    {
        private readonly List<User> _participants = [];
        private readonly List<Message> _messages = [];

        private Chat(ChatId chatId, ChatName chatName)
            : base(chatId)
        {
            this.ChatName = chatName;
        }

        private Chat() { }

        public ChatName ChatName { get; private set; }

        public UserId CreatorId { get; private set; }

        public User Creator { get; private set; }

        public IReadOnlyCollection<User> Participants => this._participants.AsReadOnly();

        public IReadOnlyCollection<Message> Messages => this._messages.AsReadOnly();

        public void AddParticipant(User user)
        {
            if (!this._participants.Contains(user))
            {
                this._participants.Add(user);
            }
        }

        public static Result<Chat> Create(ChatName chatName)
        {
            if (chatName is null || string.IsNullOrEmpty(chatName.Value))
            {
                return Result.Failure<Chat>(ChatErrors.InvalidName);
            }

            return new Chat(ChatId.New(), chatName);
        }

        public void SetCreator(User creator)
        {
            this.Creator =
                creator
                ?? throw new ArgumentNullException(nameof(creator), "Creator cannot be null");

            this.CreatorId = creator.Id;
        }

        public void AddMessage(Message message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message), "Message cannot be null");
            }

            if (!this._messages.Contains(message))
            {
                this._messages.Add(message);
            }
        }
    }
}
