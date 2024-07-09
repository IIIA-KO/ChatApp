using ChatApp.Domain.Abstraction;

namespace ChatApp.Domain.Chats
{
    public static class ChatErrors
    {
        public static readonly Error NotFound =
            new("Chat.NotFound", "The chat with the specified identifier was not found");

        public static readonly Error InvalidName =
            new("Chat.InvalidName", "The provided chat name were invalid");

        public static readonly Error NotAuthorized =
            new("Chat.NotAuthorized", "You are not authorized for this action");

        public static readonly Error AlreadyConnected =
            new("Chat.AlreadyConnected", "You are already connected to this chat");

        public static readonly Error NotConnected =
            new("Chat.NotConnected", "You are not connected to this chat");
    }
}
