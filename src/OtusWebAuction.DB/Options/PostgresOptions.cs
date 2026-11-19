using Npgsql;

namespace OtusWebAuction.DB.Options
{
    public class PostgresOptions
    {
        public const string SectionName = "Postgres";
        public required string Host { get; set; }
        public required int Port { get; set; }
        public required string Database { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        public string BuildConnectionString() =>
            new NpgsqlConnectionStringBuilder
            {
                Host = Host,
                Port = Port,
                Database = Database,
                Username = Username,
                Password = Password
            }.ConnectionString;
    }
}