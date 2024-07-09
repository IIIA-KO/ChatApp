using System.Text.Json.Serialization;

namespace ChatApp.Api.Controllers.Messages
{
    public sealed class EditMessageRequest
    {
        public EditMessageRequest(Guid userId, Guid chatId, string content)
        {
            this.UserId = userId;
            this.ChatId = chatId;
            this.Content = content;
        }

        [JsonRequired]
        public Guid UserId { get; init; }

        [JsonRequired]
        public Guid ChatId { get; init; }
        public string Content { get; init; }
    }
}
