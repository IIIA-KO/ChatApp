namespace ChatApp.Application.Chats.GetChatMessages
{
    public sealed class MessageResponse
    {
        public Guid Id { get; init; }

        public string Content { get; init; }

        public DateTime SentAt { get; init; }
    }
}
