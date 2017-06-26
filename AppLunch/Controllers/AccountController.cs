using AppLunch.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppLunch.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();
        private SignInManager<IdentityUser, string> _signInManager => HttpContext.GetOwinContext().Get<SignInManager<IdentityUser, string>>();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var status = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

            if (status == SignInStatus.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View(model);
            }
        }
    }
}