using AppLunch.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppLunch.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();
        private SignInManager<IdentityUser, string> _signInManager => HttpContext.GetOwinContext().Get<SignInManager<IdentityUser, string>>();
        private RoleManager<IdentityRole> _roleManager => HttpContext.GetOwinContext().Get<RoleManager<IdentityRole>>();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser(model.UserName) { Email = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var confirmUrl = Url.Action("ConfirmEmail", "Account", new { userid = user.Id, token = token }, Request.Url.Scheme);
                    await _userManager.SendEmailAsync(user.Id, "Lunch Generator App Email Confirmation", $"Click <a href='{confirmUrl}'>here</a> to confirm your email.");
                    return RedirectToAction("Login", new { isRegistrationRedirect = true });
                }
                else
                {
                    ModelState.AddModelError("Registration", result.Errors.First());
                }
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
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ResendEmailConfirmation(string userID)
        {
            ViewBag.UserID = userID;
            return View();
        }

        [HttpPost]
        [ActionName("ResendEmailConfirmation")]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> ResendEmailConfirmation_Post(string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);

            if (user != null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var confirmUrl = Url.Action("ConfirmEmail", "Account", new { userid = user.Id, token = token }, Request.Url.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Lunch Generator App Email Confirmation", $"Click <a href='{confirmUrl}'>here</a> to confirm your email.");
                return RedirectToAction("Login", new { isRegistrationRedirect = true });
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login(bool? isRegistrationRedirect)
        {
            var vm = new LoginModel() { isRegistrationRedirect = isRegistrationRedirect ?? false };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> Login(LoginModel model)
        {
            ViewBag.isRegistrationRedirect = false;

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && !user.EmailConfirmed)
            {
                model.needsEmailConfirmed = true;
                ViewBag.UserID = user.Id;
                ModelState.AddModelError("EmailConfirmation","Email has not been confirmed.");
            }

            if (ModelState.IsValid)
            {
                var status = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

                if (status == SignInStatus.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
            }
            return View(model);
        }
    }
}