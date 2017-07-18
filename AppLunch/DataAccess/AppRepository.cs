using AppLunch.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AppLunch.DataAccess
{
    public class AppRepository : IAppRepository
    {
        public async Task CreateMemberAsync(Member member)
        {
            using (var ctx = new AppContext())
            {
                ctx.Members.Add(member);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task CreateVenueAsync(Venue venue, int locationID)
        {
            using (var ctx = new AppContext())
            {
                venue.Locations.Add(ctx.Locations.Where(x => x.ID == locationID).Single());
                ctx.Venues.Add(venue);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task DeleteLocationAsync(int id)
        {
            using (var ctx = new AppContext())
            {
                var temp = new Location() { ID = id };
                ctx.Locations.Attach(temp);
                ctx.Locations.Remove(temp);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<Invite> GetInviteByIDAsync(Guid id)
        {
            using (var ctx = new AppContext())
            {
                return await ctx.Invites.Where(x => x.ID == id).SingleAsync();
            }
        }

        public async Task<List<Location>> GetLocationsAsync()
        {
            using (var ctx = new AppContext())
            {
                return await ctx.Locations.ToListAsync();
            }
        }

        public async Task<Member> GetMemberByIdentityIdAsync(string identityID)
        {
            using (var ctx = new AppContext())
            {
                return await ctx.Members.Where(x => x.IdentityID == identityID).SingleAsync();
            }
        }

        public async Task<List<Member>> GetMembersAsync()
        {
            using (var ctx = new AppContext())
            {
                return await ctx.Members.ToListAsync();
            }
        }

        public async Task<Venue> GetVenueByIdAsync(int id)
        {
            using (var ctx = new AppContext())
            {
                return await ctx.Venues.Where(x => x.ID == id).SingleAsync();
            }
        }

        public async Task<List<Venue>> GetVenuesByLocationIdAsync(int locationID)
        {
            using (var ctx = new AppContext())
            {
                return await ctx.Venues.Where(x => x.Locations.Any(l => l.ID == locationID)).ToListAsync();
            }
        }

        public async Task<Invite> InsertInviteAsync(Invite invite)
        {
            using (var ctx = new AppContext())
            {
                ctx.Invites.Add(invite);
                await ctx.SaveChangesAsync();
                return invite;
            }
        }

        public async Task<Location> InsertLocationAsync(Location location)
        {
            using (var ctx = new AppContext())
            {
                ctx.Locations.Add(location);
                await ctx.SaveChangesAsync();
                return location;
            }
        }

        public Task UpdateLocationAsync(Location location)
        {
            throw new NotImplementedException();
        }

        public Task UpdateVenueAsync(Venue venue)
        {
            throw new NotImplementedException();
        }
    }
}