using Microsoft.EntityFrameworkCore;
using OtusWebAuction.DB.Models;

namespace OtusWebAuction.DB.Context
{
    public sealed class AuctionDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AuctionDbContext(DbContextOptions<AuctionDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuctionDbContext).Assembly);
    }
}