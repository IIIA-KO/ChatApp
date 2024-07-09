using System.Text.Json.Serialization;

namespace ChatApp.Api.Controllers.Chats
{
    public sealed class DisconnectUserFromChatRequest
    {
        [JsonRequired]
        public Guid UserId { get; init; }
    }
}
