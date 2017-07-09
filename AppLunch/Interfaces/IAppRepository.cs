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
        List<Member> GetMembers();
    }
}
