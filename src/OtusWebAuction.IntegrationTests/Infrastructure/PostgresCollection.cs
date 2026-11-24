namespace OtusWebAuction.IntegrationTests.Infrastructure
{
#pragma warning disable CA1711
    [CollectionDefinition(Name)]
    public sealed class PostgresCollection : ICollectionFixture<PostgresContainerFixture>
#pragma warning restore CA1711
    {
        public const string Name = "Postgres";
    }
}
