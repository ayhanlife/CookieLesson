using Cookie.UI.Configuration;
using Cookie.UI.Data;
using Microsoft.EntityFrameworkCore;

namespace Cookie.UI.Context
{
    public class CookieContext : DbContext
    {

     
        public CookieContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserRoleConfiguration());
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppUserRole> UserRoles { get; set; }
    }
}
