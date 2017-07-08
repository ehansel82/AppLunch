using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AppLunch.DataAccess
{
    public class AppLunchDbContext : IdentityDbContext<AppLunchUser>
    {
        public AppLunchDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer<AppLunchDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}