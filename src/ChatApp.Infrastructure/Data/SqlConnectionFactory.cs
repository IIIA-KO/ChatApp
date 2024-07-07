using System.Data;
using ChatApp.Application.Abstraction.Data;
using Npgsql;

namespace ChatApp.Infrastructure.Data
{
    internal sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new NpgsqlConnection(this._connectionString);
            connection.Open();

            return connection;
        }
    }
}
