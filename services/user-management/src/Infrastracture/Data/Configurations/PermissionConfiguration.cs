
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PermissionId)
                .HasColumnName("PermissionId")
                .IsRequired(false);

            builder.Property(p => p.Active)
                .HasColumnName("Active")
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .IsRequired()
                .HasMaxLength(500);

            builder.OwnsOne(p => p.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("PermissionName")
                    .IsRequired();
            });

            builder.Navigation(p => p.Roles)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
