using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.SignalR
{
    public class ChatHub : Hub<IChatClient>, IChatServer
    {
        public async Task SendMessage(string chatId, string user, string message)
        {
            await this.Clients.Group(chatId).ReceiveMessage(user, message);
        }

        public async Task EditMessage(string chatId, string messageId, string newContent)
        {
            await this.Clients.Group(chatId).MessageEdited(messageId, newContent);
        }

        public async Task DeleteMessage(string chatId, string messageId)
        {
            await this.Clients.Group(chatId).MessageDeleted(messageId);
        }

        public async Task JoinChat(string chatId)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, chatId);
        }

        public async Task LeaveChat(string chatId)
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, chatId);
        }
    }
}
