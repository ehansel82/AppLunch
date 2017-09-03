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

        public DbSet<Location> Locations { get; set; }

        public DbSet<Invite> Invites { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<Ranking> Rankings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>()
            .HasMany<Venue>(s => s.Venues)
            .WithMany(c => c.Locations)
            .Map(cs =>
            {
                cs.MapLeftKey("LocationRefId");
                cs.MapRightKey("VenueRefId");
                cs.ToTable("LocationVenue", "AppLunch");
            });

            modelBuilder.Entity<Ranking>()
                .HasRequired<Member>(x => x.Member)
                .WithMany(x => x.Rankings);

            modelBuilder.Entity<Ranking>()
                .HasRequired<Venue>(x => x.Venue)
                .WithMany(x => x.Rankings);

        }
    }
}