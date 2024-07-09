namespace ChatApp.Api.SignalR
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);

        Task MessageEdited(string messageId, string newContent);

        Task MessageDeleted(string messageId);

        Task ChatDeleted(string chatId);
    }
}
