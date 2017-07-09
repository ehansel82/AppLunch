using AppLunch.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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

        public async Task<List<Member>> GetMembersAsync()
        {
            using (var ctx = new AppContext())
            {
                return await ctx.Members.ToListAsync();
            }
        }
    }
}