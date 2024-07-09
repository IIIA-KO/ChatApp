using System.Text.Json.Serialization;

namespace ChatApp.Api.Controllers.Messages
{
    public sealed class DeleteMessageRequest
    {
        public DeleteMessageRequest(Guid userId, Guid chatId)
        {
            this.UserId = userId;
            this.ChatId = chatId;
        }

        [JsonRequired]
        public Guid UserId { get; init; }

        [JsonRequired]
        public Guid ChatId { get; init; }
    }
}
