using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtusWebAuction.DB.Models;

namespace OtusWebAuction.DB.Configurations
{
    public sealed class LotConfiguration : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.ToTable("Lots");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.SellerId).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.StartPrice);
            builder.Property(x => x.BuyoutPrice);
            builder.Property(x => x.MinStep);
            builder.Property(x => x.Status);
            builder.Property(x => x.ModerationStatus);
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.LastUpdatedDate).IsRequired();
            builder.Property(x => x.DeleteDate);

            // Мягкое удаление: удалённые лоты не попадают в обычные запросы.
            // Если нужны и удалённые записи (админские сценарии), используйте IgnoreQueryFilters().
            builder.HasQueryFilter(x => x.DeleteDate == null);
        }
    }
}
