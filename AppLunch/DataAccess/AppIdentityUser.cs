using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppLunch.DataAccess
{
    public class AppIdentityUser : IdentityUser
    {
        public AppIdentityUser() : base()
        {

        }

        public AppIdentityUser(string userName) : base(userName)
        {
        }

        
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }
        
    }
}