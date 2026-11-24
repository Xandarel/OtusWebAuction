namespace OtusWebAuction.IntegrationTests
{
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using OtusWebAuction.BL.Users.Register;

    public static class AuthTestHelper
    {
        private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
        {
            Converters = { new JsonStringEnumConverter() }
        };

        public static async Task<AuthResponse> RegisterUserAsync(HttpClient client, RegisterUserRequest? request = null)
        {
            RegisterUserRequest registerRequest = request ?? CreateUniqueRegisterRequest();

            HttpResponseMessage response = await client.PostAsJsonAsync("/User/RegisterUser", registerRequest, _jsonOptions);
            response.EnsureSuccessStatusCode();

            AuthResponse? authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>(_jsonOptions);
            if (authResponse is null)
            {
                throw new InvalidOperationException("RegisterUser returned an empty response.");
            }

            return authResponse;
        }

        public static RegisterUserRequest CreateUniqueRegisterRequest(string? emailPrefix = null)
        {
            RegisterUserRequest request = new RegisterUserRequest
            {
                Name = "Test",
                Surname = "User",
                Email = "TestUser@example.test",
                Password = "1234567"
            };

            return request;
        }

        public static void SetBearerToken(HttpClient client, string accessToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public static async Task<HttpClient> CreateAuthenticatedClientAsync(AuctionWebApplicationFactory factory)
        {
            HttpClient client = factory.CreateClient();
            AuthResponse authResponse = await RegisterUserAsync(client);
            SetBearerToken(client, authResponse.AccessToken);
            return client;
        }
    }
}
