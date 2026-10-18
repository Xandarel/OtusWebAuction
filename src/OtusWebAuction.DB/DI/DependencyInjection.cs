using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OtusWebAuction.DB.Context;

namespace OtusWebAuction.DB.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AuctionDbContext>(options => options.UseNpgsql(connectionString));
            return services;
        }
    }
}
