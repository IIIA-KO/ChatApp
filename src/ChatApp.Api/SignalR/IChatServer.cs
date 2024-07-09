namespace ChatApp.Api.SignalR
{
    public interface IChatServer
    {
        Task SendMessage(string chatId, string user, string message);
        Task EditMessage(string chatId, string messageId, string newContent);
        Task DeleteMessage(string chatId, string messageId);
        Task JoinChat(string chatId);
        Task LeaveChat(string chatId);
    }
}
