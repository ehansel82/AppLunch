using AppLunch.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task DeleteLocation(int id)
        {
            using (var ctx = new AppContext())
            {
                var temp = new Location() { ID = id };
                ctx.Locations.Attach(temp);
                ctx.Locations.Remove(temp);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Location>> GetLocationsAsync()
        {
            using (var ctx = new AppContext())
            {
                return await ctx.Locations.ToListAsync();
            }
        }

        public async Task<List<Member>> GetMembersAsync()
        {
            using (var ctx = new AppContext())
            {
                return await ctx.Members.ToListAsync();
            }
        }

        public async Task<Location> InsertLocation(Location location)
        {
            using (var ctx = new AppContext())
            {
                ctx.Locations.Add(location);
                await ctx.SaveChangesAsync();
                return location;
            }
        }

        public Task UpdateLocation(Location location)
        {
            throw new NotImplementedException();
        }
    }
}