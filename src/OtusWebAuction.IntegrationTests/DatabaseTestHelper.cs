namespace OtusWebAuction.IntegrationTests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using OtusWebAuction.DB.Context;
    using OtusWebAuction.DB.Models;

    public static class DatabaseTestHelper
    {
        public static async Task<int> EnsureTestCategoryAsync(AuctionWebApplicationFactory factory)
        {
            using IServiceScope scope = factory.Services.CreateScope();
            AuctionDbContext dbContext = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();

            Category? existingCategory = await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync();
            if (existingCategory is not null)
            {
                return existingCategory.Id;
            }

            await dbContext.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Categories\" (\"Id\", \"ParentId\", \"Name\") OVERRIDING SYSTEM VALUE VALUES (1, 1, 'Test category');");

            return 1;
        }
    }
}
