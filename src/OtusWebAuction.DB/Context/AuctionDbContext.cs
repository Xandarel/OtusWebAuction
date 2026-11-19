using Microsoft.EntityFrameworkCore;
using OtusWebAuction.DB.Models;

namespace OtusWebAuction.DB.Context
{
    public sealed class AuctionDbContext : DbContext
    {
        /// <summary>
        /// Список пользователей
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Категории товаров
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Лоты аукциона
        /// </summary>
        public DbSet<Lot> Lots { get; set; }

        /// <summary>
        /// Ставки на лоты аукциона
        /// </summary>
        public DbSet<Bet> Bets { get; set; }

        public DbSet<Role> Roles { get; set; }

        public AuctionDbContext(DbContextOptions<AuctionDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuctionDbContext).Assembly);
    }
}