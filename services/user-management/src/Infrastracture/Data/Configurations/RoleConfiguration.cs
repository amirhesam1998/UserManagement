using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObject;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastracture.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            //var roleNameConverter = new ValueConverter<RoleName, string>(
            //    v => v.Value,
            //    v => RoleName.Create(v).Value!);

            //builder.Property(r => r.Title)
            //    .HasConversion(roleNameConverter)
            //    .HasColumnName("Title")
            //    .IsRequired();
            builder.Property(u => u.Title).HasColumnName("Title").IsRequired();
            builder.Property(u => u.Description).HasColumnName("Description").IsRequired();
            
            builder.HasMany(r => r.Users)
            .WithMany(u => u.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "UserRoles",
                j => j.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId"),
                j => j.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId"),
                j =>
                {
                    j.HasKey("UserId", "RoleId");
                    j.ToTable("UserRoles");
                });
        }
    }
}
