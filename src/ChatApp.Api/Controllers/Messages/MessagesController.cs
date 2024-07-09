using ChatApp.Api.SignalR;
using ChatApp.Application.Messages.CreateMessage;
using ChatApp.Application.Messages.DeleteMessage;
using ChatApp.Application.Messages.EditMessage;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Messages;
using ChatApp.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Controllers.Messages
{
    [Route("api/messages")]
    public class MessagesController : BaseApiController
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;

        public MessagesController(IHubContext<ChatHub, IChatClient> chatHub)
        {
            this._chatHub = chatHub;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(
            Guid chatId,
            [FromBody] CreateMessageRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateMessageCommand(
                new UserId(request.UserId),
                new ChatId(chatId),
                new Content(request.Content)
            );

            Result result = await this.Sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                await this
                    ._chatHub.Clients.Group(chatId.ToString())
                    .ReceiveMessage(request.UserId.ToString(), request.Content);
            }

            return this.HandleResult(result);
        }

        [HttpPut("{messageId:guid}/edit")]
        public async Task<IActionResult> EditMessage(
            Guid messageId,
            [FromBody] EditMessageRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new EditMessageCommand(
                new UserId(request.UserId),
                new MessageId(messageId),
                new Content(request.Content)
            );

            Result result = await this.Sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                await this
                    ._chatHub.Clients.Group(request.ChatId.ToString())
                    .MessageEdited(messageId.ToString(), request.Content);
            }

            return this.HandleResult(result);
        }

        [HttpDelete("{messageId:guid}/delete")]
        public async Task<IActionResult> DeleteMessage(
            Guid messageId,
            [FromBody] DeleteMessageRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var command = new DeleteMessageCommand(
                new UserId(request.UserId),
                new MessageId(messageId)
            );

            Result result = await this.Sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                await this
                    ._chatHub.Clients.Group(request.ChatId.ToString())
                    .MessageDeleted(messageId.ToString());
            }

            return this.HandleResult(result);
        }
    }
}
