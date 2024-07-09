using ChatApp.Domain.Abstraction;

namespace ChatApp.Domain.Messages
{
    public static class MessageErrors
    {
        public static readonly Error NotFound =
            new("Message.NotFound", "The message with the specified identifier was not found");

        public static readonly Error InvalidContent =
            new("Message.InvalidContent", "The provided content were invalid");

        public static readonly Error NotAuthorized =
            new(
                "Message.NotAuthorized",
                "You are not authorized to perform this action with this message"
            );
    }
}
