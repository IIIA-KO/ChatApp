using ChatApp.Domain.Chats;

namespace ChatApp.Infrastructure.Specifications.Chats
{
    internal sealed class ChatByIdWithParticipantsSpecification : Specification<Chat, ChatId>
    {
        public ChatByIdWithParticipantsSpecification(ChatId chatId)
            : base(chat => chat.Id == chatId)
        {
            this.AddInclude(chat => chat.Participants);
        }
    }
}
