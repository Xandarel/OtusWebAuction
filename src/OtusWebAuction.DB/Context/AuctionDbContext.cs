using Microsoft.EntityFrameworkCore;

namespace OtusWebAuction.DB.Context
{
    public sealed class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options)
            : base(options)
        {
        }
    }
}
