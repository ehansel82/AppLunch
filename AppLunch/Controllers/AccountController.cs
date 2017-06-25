using AppLunch.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace AppLunch.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();

        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegistrationModel());
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistrationModel model)
        {
            var result = await _userManager.CreateAsync(new IdentityUser(model.UserName), model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
            
        }



        
    }
}