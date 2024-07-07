namespace ChatApp.Application.Abstraction.Clock
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
