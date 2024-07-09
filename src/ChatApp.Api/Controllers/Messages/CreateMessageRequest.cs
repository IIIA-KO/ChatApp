using System.Text.Json.Serialization;

namespace ChatApp.Api.Controllers.Messages
{
    public sealed class CreateMessageRequest
    {
        public CreateMessageRequest(Guid UserId, string Content)
        {
            this.UserId = UserId;
            this.Content = Content;
        }

        [JsonRequired]
        public Guid UserId { get; init; }

        public string Content { get; init; }
    }
}
