using AppLunch.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppLunch.Models
{
    [NotMapped]
    public class ManageUsersModel : Member
    {
        public bool isManager { get; set; }
    }
}