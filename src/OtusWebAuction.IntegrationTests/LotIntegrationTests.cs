using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using OtusWebAuction.BL.Lots.Dto;
using OtusWebAuction.BL.Lots.Request;
using OtusWebAuction.BL.Users.Register;
using OtusWebAuction.IntegrationTests.Infrastructure;

namespace OtusWebAuction.IntegrationTests
{

    [Collection(PostgresCollection.Name)]
    public sealed class LotIntegrationTests : DbIntegrationTest
    {
        private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
        {
            Converters = { new JsonStringEnumConverter() }
        };

        public LotIntegrationTests(PostgresContainerFixture postgresContainerFixture)
            : base(postgresContainerFixture)
        {
        }

        [Fact]
        public async Task CreateLot_WithValidRequest_ReturnsCreatedLot()
        {
            int categoryId = await DatabaseTestHelper.EnsureTestCategoryAsync(Factory);

            AuthResponse authResponse = await AuthTestHelper.RegisterUserAsync(Client);
            AuthTestHelper.SetBearerToken(Client, authResponse.AccessToken);

            CreateLotRequest request = new CreateLotRequest
            {
                Name = "Золотые часы 19 век",
                Description = "Наручные золотые часы 19 век, женские",
                CategoryId = categoryId,
                StartPrice = 100m,
                BuyoutPrice = 500m,
                MinStep = 10m
            };

            HttpResponseMessage response = await Client.PostAsJsonAsync("/Lot/CreateLot", request, _jsonOptions);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            LotDto? lot = await response.Content.ReadFromJsonAsync<LotDto>(_jsonOptions);

            Assert.NotNull(lot);
            Assert.True(lot.Id > 0);
            Assert.Equal(request.Name, lot.Name);
            Assert.Equal(request.Description, lot.Description);
            Assert.Equal(categoryId, lot.CategoryId);
            Assert.Equal(authResponse.User.Id, lot.SellerId);
            Assert.Equal(request.StartPrice, lot.StartPrice);
            Assert.Equal(request.BuyoutPrice, lot.BuyoutPrice);
            Assert.Equal(request.MinStep, lot.MinStep);
            Assert.Null(lot.ModerationStatus);
        }
    }
}
