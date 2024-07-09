using System.Text.Json.Serialization;

namespace ChatApp.Api.Controllers.Chats
{
    public sealed class DeleteChatRequest
    {
        [JsonRequired]
        public Guid UserId { get; init; }
    }
}
