
using Domain.Entities;
using Domain.ValueObject;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);


            builder.Property(u => u.FirstName).HasColumnName("FirstName").IsRequired();
            builder.Property(u => u.LastName).HasColumnName("LastName").IsRequired();
            builder.Property(u => u.Password).HasColumnName("Password").IsRequired();
            builder.Property(u => u.CreatedAt).HasColumnName("CreatedAt").IsRequired();

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Value).HasColumnName("Email").IsRequired();
            });


            builder.OwnsOne(u => u.Username, username =>
            {
                username.Property(p => p.Value).HasColumnName("UserName").IsRequired();
            });

            builder.Property(u => u.Type)
                .HasColumnName("UserType")
                .HasConversion<string>()
                .IsRequired();

            builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRoles",
                j => j.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId"),
                j => j.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId"),
                j =>
                {
                    j.HasKey("UserId", "RoleId");
                    j.ToTable("UserRoles");
                });


            builder.Navigation(u => u.Username).IsRequired();

            //builder.Navigation("_roles").HasField("_roles").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
