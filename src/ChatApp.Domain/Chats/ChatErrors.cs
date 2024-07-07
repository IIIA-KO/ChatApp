using ChatApp.Domain.Abstraction;

namespace ChatApp.Domain.Chats
{
    public static class ChatErrors
    {
        public static readonly Error NotFound =
            new("Chat.NotFound", "The chat with the specified identifier was not found");

        public static readonly Error InvalidName =
            new("Chat.InvalidName", "The provided chat name were invalid");
    }
}
