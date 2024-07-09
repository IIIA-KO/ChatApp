using System.Data;
using ChatApp.Application.Abstraction.Data;
using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Abstraction;
using Dapper;

namespace ChatApp.Application.Chats.GetChatMessages
{
    internal sealed class GetChatMessagesQueryHandler
        : IQueryHandler<GetChatMessagesQuery, IReadOnlyList<MessageResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetChatMessagesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<MessageResponse>>> Handle(
            GetChatMessagesQuery request,
            CancellationToken cancellationToken
        )
        {
            using IDbConnection dbConnection = this._sqlConnectionFactory.CreateConnection();

            const string sql = """
                SELECT
                    id AS Id,
                    content AS Content,
                    sent_at AS SentAt
                FROM messages
                WHERE chat_id = @ChatId
                """;

            return (
                await dbConnection.QueryAsync<MessageResponse>(
                    sql,
                    new { ChatId = request.ChatId.Value }
                )
            ).ToList();
        }
    }
}
