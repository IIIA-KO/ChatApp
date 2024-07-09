using System.Text.Json.Serialization;

namespace ChatApp.Api.Controllers.Chats
{
    public sealed class ConnectUserToChatRequest
    {
        [JsonRequired]
        public Guid UserId { get; init; }
    }
}
