using AppLunch.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppLunch.Models
{
    public class ManageUsersModel
    {
        public List<AppIdentityUser> Users { get; set; }
    }
}