namespace OtusWebAuction.IntegrationTests.Infrastructure
{
    using Npgsql;

    public sealed class TestDatabase
    {
        private readonly PostgresContainerFixture _containerFixture;
        private readonly string _databaseName;

        public TestDatabase(PostgresContainerFixture containerFixture, string databaseName)
        {
            _containerFixture = containerFixture;
            _databaseName = databaseName;
            ConnectionString = containerFixture.GetConnectionString(databaseName);
        }

        public string ConnectionString { get; }

        public async Task CreateAsync()
        {
            await using NpgsqlDataSource dataSource = NpgsqlDataSource.Create(_containerFixture.GetAdminConnectionString());
            await using NpgsqlCommand command = dataSource.CreateCommand(
                $"CREATE DATABASE \"{_databaseName}\" TEMPLATE \"{PostgresContainerFixture.TemplateDatabaseName}\";");
            await command.ExecuteNonQueryAsync();
        }

        public async Task DropAsync()
        {
            await using NpgsqlDataSource dataSource = NpgsqlDataSource.Create(_containerFixture.GetAdminConnectionString());
            await using NpgsqlCommand command = dataSource.CreateCommand(
                $"DROP DATABASE IF EXISTS \"{_databaseName}\" WITH (FORCE);");
            await command.ExecuteNonQueryAsync();
        }
    }
}
