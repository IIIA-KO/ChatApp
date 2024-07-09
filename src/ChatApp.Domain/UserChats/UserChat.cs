using ChatApp.Domain.Chats;
using ChatApp.Domain.Users;

namespace ChatApp.Domain.UserChats
{
    public sealed class UserChat
    {
        public UserId UserId { get; set; }
        public User User { get; set; }

        public ChatId ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
