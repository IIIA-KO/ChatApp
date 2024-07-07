using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;

namespace ChatApp.Domain.Users
{
    public sealed class User : Entity<UserId>
    {
        private readonly List<Chat> _createdChats = [];
        private readonly List<Chat> _chats = [];

        private User() { }

        private User(UserId id, UserName userName)
            : base(id)
        {
            this.UserName = userName;
        }

        public UserName UserName { get; private set; }

        public IReadOnlyCollection<Chat> CreatedChats => this._createdChats.AsReadOnly();

        public IReadOnlyCollection<Chat> Chats => this._chats.AsReadOnly();

        public void AddCreatedChat(Chat chat)
        {
            if (!this._createdChats.Contains(chat))
            {
                this._createdChats.Add(chat);
            }
        }

        public void AddChat(Chat chat)
        {
            if (!this._chats.Contains(chat))
            {
                this._chats.Add(chat);
            }
        }

        public void RemoveChat(Chat chat)
        {
            this._chats.Remove(chat);
        }

        public static Result<User> Create(UserName userName)
        {
            if (userName is null || string.IsNullOrEmpty(userName.Value))
            {
                return Result.Failure<User>(UserErrors.InvalidCredentials);
            }

            var user = new User(UserId.New(), userName);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            return user;
        }
    }
}
