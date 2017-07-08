using AppLunch.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppLunch.Controllers
{
    public class AdminController : Controller
    {

        private RoleManager<IdentityRole> _roleManager => HttpContext.GetOwinContext().Get<RoleManager<IdentityRole>>();
        private UserManager<AppIdentityUser> _userManager => HttpContext.GetOwinContext().Get<UserManager<AppIdentityUser>>();

        [HttpGet]
        public ActionResult ManageUsers()
        {

            return View();
        }
    }
}