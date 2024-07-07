using ChatApp.Domain.Users;

namespace ChatApp.Infrastructure.Specifications.Users
{
    internal sealed class UserByIdWithCreatedChatsSpecification : Specification<User, UserId>
    {
        public UserByIdWithCreatedChatsSpecification(UserId userId)
            : base(user => user.Id == userId)
        {
            this.AddInclude(user => user.CreatedChats);
        }
    }
}
