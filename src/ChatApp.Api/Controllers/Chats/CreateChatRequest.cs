using System.Text.Json.Serialization;

namespace ChatApp.Api.Controllers.Chats
{
    public sealed class CreateChatRequest
    {
        [JsonRequired]
        public Guid UserId { get; init; }

        public string ChatName { get; init; }
    }
}
