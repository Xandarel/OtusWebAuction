namespace OtusWebAuction.IntegrationTests.Infrastructure
{
    [Collection(PostgresCollection.Name)]
    public abstract class DbIntegrationTest : IAsyncLifetime
    {
        private TestDatabase? _testDatabase;

        protected DbIntegrationTest(PostgresContainerFixture postgresContainerFixture)
        {
            PostgresContainerFixture = postgresContainerFixture;
        }

        protected PostgresContainerFixture PostgresContainerFixture { get; }

        protected AuctionWebApplicationFactory Factory { get; private set; } = null!;

        protected HttpClient Client { get; private set; } = null!;

        public async Task InitializeAsync()
        {
            string databaseName = $"auction_test_{Guid.NewGuid():N}";
            _testDatabase = new TestDatabase(PostgresContainerFixture, databaseName);
            await _testDatabase.CreateAsync();

            Factory = new AuctionWebApplicationFactory
            {
                ConnectionString = _testDatabase.ConnectionString
            };
            Client = Factory.CreateClient();
        }

        public async Task DisposeAsync()
        {
            Client.Dispose();
            await Factory.DisposeAsync();

            if (_testDatabase is not null)
            {
                await _testDatabase.DropAsync();
            }
        }
    }
}
