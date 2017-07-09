using AppLunch.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLunch.Interfaces
{
    public interface IAppRepository
    {
        Task<List<Member>> GetMembersAsync();

        Task CreateMemberAsync(Member member);
    }
}
