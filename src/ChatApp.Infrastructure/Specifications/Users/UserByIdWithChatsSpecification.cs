using ChatApp.Domain.Users;

namespace ChatApp.Infrastructure.Specifications.Users
{
    internal sealed class UserByIdWithChatsSpecification : Specification<User, UserId>
    {
        public UserByIdWithChatsSpecification(UserId userId)
            : base(user => user.Id == userId)
        {
            this.AddInclude(user => user.Chats);
        }
    }
}
