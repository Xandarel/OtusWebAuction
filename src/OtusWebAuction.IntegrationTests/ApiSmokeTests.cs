namespace OtusWebAuction.IntegrationTests
{
    using System.Net;

    public class ApiSmokeTests : IClassFixture<AuctionWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ApiSmokeTests(AuctionWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetSwaggerDocument_ReturnsOk()
        {
            HttpResponseMessage response = await _client.GetAsync("/swagger/v1/swagger.json");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
