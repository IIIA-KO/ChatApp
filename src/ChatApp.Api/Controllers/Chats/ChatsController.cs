using ChatApp.Application.Chats.CreateChat;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers.Chats
{
    [Route("api/chats")]
    public class ChatsController : BaseApiController
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateChat(
            [FromBody] CreateChatRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateChatCommand(
                new UserId(request.UserId),
                new ChatName(request.ChatName)
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
