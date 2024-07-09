namespace ChatApp.Application.Chats.GetAllChats
{
    public class ChatResponse
    {
        public ChatResponse() { }

        public ChatResponse(Guid Id, string ChatName, Guid CreatorId)
        {
            this.Id = Id;
            this.ChatName = ChatName;
            this.CreatorId = CreatorId;
        }

        public Guid Id { get; init; }
        public string ChatName { get; init; }
        public Guid CreatorId { get; init; }
    }
}
