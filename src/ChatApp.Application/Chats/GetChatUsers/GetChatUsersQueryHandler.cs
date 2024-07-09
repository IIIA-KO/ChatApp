using System.Data;
using ChatApp.Application.Abstraction.Data;
using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Abstraction;
using Dapper;

namespace ChatApp.Application.Chats.GetChatUsers
{
    internal sealed class GetChatUsersQueryHandler
        : IQueryHandler<GetChatUsersQuery, IReadOnlyList<UserResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetChatUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<UserResponse>>> Handle(
            GetChatUsersQuery request,
            CancellationToken cancellationToken
        )
        {
            using IDbConnection dbConnection = this._sqlConnectionFactory.CreateConnection();

            const string sql = """
                SELECT
                	u.id AS Id,
                    u.user_name AS UserName
                FROM users_chats uc
                INNER JOIN users u 
                    ON uc.user_id = u.id 
                WHERE chat_id = @ChatId
                """;

            return (
                await dbConnection.QueryAsync<UserResponse>(
                    sql,
                    new { ChatId = request.ChatId.Value }
                )
            ).ToList();
        }
    }
}
