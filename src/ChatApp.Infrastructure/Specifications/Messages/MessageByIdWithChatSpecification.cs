using ChatApp.Domain.Messages;

namespace ChatApp.Infrastructure.Specifications.Messages
{
    internal sealed class MessageByIdWithChatSpecification : Specification<Message, MessageId>
    {
        public MessageByIdWithChatSpecification(MessageId messageId)
            : base(message => message.Id == messageId)
        {
            this.AddInclude(message => message.Chat);
        }
    }
}
