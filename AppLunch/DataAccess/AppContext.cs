using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AppLunch.DataAccess
{
    public class AppContext : IdentityDbContext<AppIdentityUser>
    {
        public AppContext() : base("DefaultConnection")
        {
            Database.SetInitializer<AppContext>(null);
        }

        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}