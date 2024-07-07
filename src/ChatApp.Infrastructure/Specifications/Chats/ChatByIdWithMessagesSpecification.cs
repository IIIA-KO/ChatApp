using ChatApp.Domain.Chats;

namespace ChatApp.Infrastructure.Specifications.Chats
{
    internal sealed class ChatByIdWithMessagesSpecification : Specification<Chat, ChatId>
    {
        public ChatByIdWithMessagesSpecification(ChatId chatId)
            : base(chat => chat.Id == chatId)
        {
            this.AddInclude(chat => chat.Messages);
        }
    }
}
