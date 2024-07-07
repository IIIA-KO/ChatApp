namespace ChatApp.Domain.Chats
{
    public sealed record ChatId(Guid Value)
    {
        public static ChatId New() => new(Guid.NewGuid());
    }
}
