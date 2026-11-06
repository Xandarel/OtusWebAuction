using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtusWebAuction.DB.Models;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.DB.Configurations
{
    public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RoleName)
                .IsRequired()
                .HasConversion<string>();

            builder.HasIndex(x => x.RoleName).IsUnique();

            builder.HasData(
                new Role { Id = 1, RoleName = UserRole.User },
                new Role { Id = 2, RoleName = UserRole.Admin });
        }
    }
}
