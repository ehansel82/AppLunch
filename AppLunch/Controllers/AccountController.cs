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
            var user = new IdentityUser(model.UserName) { Email = model.UserName };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var confirmUrl = Url.Action("ConfirmEmail", "Account", new { userid = user.Id, token = token }, Request.Url.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Lunch Club App Email Confirmation", $"Use this link to confirm your email {confirmUrl}.");
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> ConfirmEmail(string userid, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(userid, token);
            if (!result.Succeeded)
            {
                return View("Error");
            }
            return RedirectToAction("Index", "Home");
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