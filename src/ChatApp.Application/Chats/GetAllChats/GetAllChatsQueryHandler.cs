using System.Data;
using ChatApp.Application.Abstraction.Data;
using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Abstraction;
using Dapper;

namespace ChatApp.Application.Chats.GetAllChats
{
    internal sealed class GetAllChatsQueryHandler
        : IQueryHandler<GetAllChatsQuery, IReadOnlyList<ChatResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllChatsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<ChatResponse>>> Handle(
            GetAllChatsQuery request,
            CancellationToken cancellationToken
        )
        {
            using IDbConnection dbConnection = this._sqlConnectionFactory.CreateConnection();

            const string sql = """
                SELECT
                    id AS Id,
                    chat_name AS ChatName,
                    creator_id AS CreatorId
                FROM chats
                """;

            return (await dbConnection.QueryAsync<ChatResponse>(sql)).ToList();
        }
    }
}
