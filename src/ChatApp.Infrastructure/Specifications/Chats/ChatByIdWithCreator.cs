using ChatApp.Domain.Chats;

namespace ChatApp.Infrastructure.Specifications.Chats
{
    internal sealed class ChatByIdWithCreator : Specification<Chat, ChatId>
    {
        public ChatByIdWithCreator(ChatId chatId)
            : base(chat => chat.Id == chatId)
        {
            this.AddInclude(chat => chat.Creator);
        }
    }
}
