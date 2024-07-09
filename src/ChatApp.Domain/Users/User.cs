using ChatApp.Domain.Abstraction;

namespace ChatApp.Domain.Users
{
    public sealed class User : Entity<UserId>
    {
        private User() { }

        private User(UserId id, UserName userName)
            : base(id)
        {
            this.UserName = userName;
        }

        public UserName UserName { get; private set; }

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
