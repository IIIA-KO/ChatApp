namespace ChatApp.Domain.Messages
{
    public sealed record MessageId(Guid Value)
    {
        public static MessageId New() => new(Guid.NewGuid());
    }
}
