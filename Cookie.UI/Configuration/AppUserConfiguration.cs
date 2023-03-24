using Cookie.UI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookie.UI.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(new AppUser
            {
                Id = 1,
                UserName = "Ayhan",
                Password = "1"
            });
            builder.Property(x => x.Password).HasMaxLength(200).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(200).IsRequired();
        }
    }


    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(new AppRole
            {
                Id=1,
                Definiton="Admin"
            });
            builder.Property(x => x.Definiton).HasMaxLength(200).IsRequired();

        }
    }


    public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasData(new AppUserRole
            {
                RoleId = 1,
                UserId = 1,
            });
            builder.HasKey(x => new { x.UserId, x.RoleId }); // ikisi birlikte primary key
            builder.HasOne(x => x.AppRole).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);

        }
    }
}
