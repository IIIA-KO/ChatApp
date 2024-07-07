using ChatApp.Domain.Abstraction;

namespace ChatApp.Domain.Users
{
    public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
}
