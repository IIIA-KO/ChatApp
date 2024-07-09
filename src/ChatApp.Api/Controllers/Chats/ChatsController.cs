using ChatApp.Api.SignalR;
using ChatApp.Application.Chats.ConnectUserToChat;
using ChatApp.Application.Chats.CreateChat;
using ChatApp.Application.Chats.DeleteChat;
using ChatApp.Application.Chats.DisconnectUserFromChat;
using ChatApp.Application.Chats.GetAllChats;
using ChatApp.Application.Chats.GetChatMessages;
using ChatApp.Application.Chats.GetChatUsers;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Controllers.Chats
{
    [Route("api/chats")]
    public class ChatsController : BaseApiController
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;

        public ChatsController(IHubContext<ChatHub, IChatClient> chatHub)
        {
            this._chatHub = chatHub;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllChatsQuery();
            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

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

            Result<ChatId> result = await this.Sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                await this._chatHub.Groups.AddToGroupAsync(
                    request.UserId.ToString(),
                    result.Value.Value.ToString(),
                    cancellationToken
                );
            }

            return this.HandleResult(result);
        }

        [HttpGet("{chatId:guid}/messages")]
        public async Task<IActionResult> ChatMessages(
            Guid chatId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetChatMessagesQuery(new ChatId(chatId));

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpPost("{chatId:guid}/connect")]
        public async Task<IActionResult> ConnectToChat(
            Guid chatId,
            [FromBody] ConnectUserToChatRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new ConnectUserToChatCommand(
                new UserId(request.UserId),
                new ChatId(chatId)
            );

            Result result = await this.Sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                await this._chatHub.Groups.AddToGroupAsync(
                    request.UserId.ToString(),
                    chatId.ToString(),
                    cancellationToken
                );
            }

            return this.HandleResult(result);
        }

        [HttpPost("{chatId:guid}/disconnect")]
        public async Task<IActionResult> DisconnectFromChat(
            Guid chatId,
            [FromBody] DisconnectUserFromChatRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new DisconnectUserFromChatCommand(
                new UserId(request.UserId),
                new ChatId(chatId)
            );

            Result result = await this.Sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                await this._chatHub.Groups.RemoveFromGroupAsync(
                    request.UserId.ToString(),
                    chatId.ToString(),
                    cancellationToken
                );
            }

            return this.HandleResult(result);
        }

        [HttpDelete("{chatId:guid}")]
        public async Task<IActionResult> DeleteChat(
            Guid chatId,
            [FromBody] DeleteChatRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new DeleteChatCommand(new UserId(request.UserId), new ChatId(chatId));

            Result result = await this.Sender.Send(command, cancellationToken);

            return this.HandleResult(result);
        }

        private async Task<IReadOnlyList<UserResponse>> GetChatParticipants(
            Guid chatId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetChatUsersQuery(new ChatId(chatId));

            Result<IReadOnlyList<UserResponse>> result = await this.Sender.Send(
                query,
                cancellationToken
            );

            if (result.IsSuccess)
            {
                IReadOnlyList<UserResponse> users = await this.GetChatParticipants(
                    chatId,
                    cancellationToken
                );

                foreach (UserResponse user in users)
                {
                    await this._chatHub.Groups.RemoveFromGroupAsync(
                        user.Id.ToString(),
                        chatId.ToString(),
                        cancellationToken
                    );
                }

                await this._chatHub.Clients.Group(chatId.ToString()).ChatDeleted(chatId.ToString());
            }

            return result.IsSuccess ? result.Value : new List<UserResponse>().AsReadOnly();
        }
    }
}
