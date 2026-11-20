namespace OtusWebAuction.IntegrationTests.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Npgsql;
    using OtusWebAuction.DB.Context;
    using Testcontainers.PostgreSql;

    public sealed class PostgresContainerFixture : IAsyncLifetime
    {
        public const string TemplateDatabaseName = "auction_tpl";

        private readonly PostgreSqlContainer _container = new PostgreSqlBuilder("postgres:18-alpine")
            .WithDatabase(TemplateDatabaseName)
            .Build();

        public string GetConnectionString(string database)
        {
            NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder(_container.GetConnectionString())
            {
                Database = database
            };

            return connectionStringBuilder.ConnectionString;
        }

        public string GetAdminConnectionString()
        {
            return GetConnectionString("postgres");
        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();
            await ApplyMigrationsAsync();
        }

        public async Task DisposeAsync()
        {
            await _container.DisposeAsync();
        }

        private async Task ApplyMigrationsAsync()
        {
            string connectionString = GetConnectionString(TemplateDatabaseName);
            DbContextOptions<AuctionDbContext> options = new DbContextOptionsBuilder<AuctionDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            await using AuctionDbContext dbContext = new AuctionDbContext(options);
            await dbContext.Database.MigrateAsync();

            NpgsqlConnection.ClearAllPools();
        }
    }
}
