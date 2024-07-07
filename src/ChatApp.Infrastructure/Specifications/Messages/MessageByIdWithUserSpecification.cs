using ChatApp.Domain.Messages;

namespace ChatApp.Infrastructure.Specifications.Messages
{
    internal sealed class MessageByIdWithUserSpecification : Specification<Message, MessageId>
    {
        public MessageByIdWithUserSpecification(MessageId messageId)
            : base(message => message.Id == messageId)
        {
            this.AddInclude(message => message.User);
        }
    }
}
