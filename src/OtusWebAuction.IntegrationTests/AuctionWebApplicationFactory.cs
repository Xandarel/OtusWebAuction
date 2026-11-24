namespace OtusWebAuction.IntegrationTests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Configuration;
    using Npgsql;

    public class AuctionWebApplicationFactory : WebApplicationFactory<Program>
    {
        public string? ConnectionString { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                Dictionary<string, string?> testConfiguration = new Dictionary<string, string?>
                {
                    ["Postgres:Host"] = "localhost",
                    ["Postgres:Port"] = "5434",
                    ["Postgres:Database"] = "auction_test",
                    ["Postgres:Username"] = "postgres",
                    ["Postgres:Password"] = "postgres"
                };

                if (!string.IsNullOrWhiteSpace(ConnectionString))
                {
                    NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder(ConnectionString);
                    testConfiguration["Postgres:Host"] = connectionStringBuilder.Host;
                    testConfiguration["Postgres:Port"] = connectionStringBuilder.Port.ToString();
                    testConfiguration["Postgres:Database"] = connectionStringBuilder.Database;
                    testConfiguration["Postgres:Username"] = connectionStringBuilder.Username;
                    testConfiguration["Postgres:Password"] = connectionStringBuilder.Password;
                }

                configurationBuilder.AddInMemoryCollection(testConfiguration);
                configurationBuilder.AddEnvironmentVariables();
            });
        }
    }
}
