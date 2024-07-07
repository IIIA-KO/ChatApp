namespace ChatApp.Domain.Abstraction
{
    public interface IEntity
    {
        IReadOnlyList<IDomainEvent> GetDomainEvents();

        void ClearDomainEvents();
    }
}
