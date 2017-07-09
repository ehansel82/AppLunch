using AppLunch.DataAccess;
using AppLunch.Interfaces;
using AppLunch.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppLunch.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private IAuthenticationManager _authManager => HttpContext.GetOwinContext().Authentication;
        private RoleManager<IdentityRole> _roleManager => HttpContext.GetOwinContext().Get<RoleManager<IdentityRole>>();
        private SignInManager<AppIdentityUser, string> _signInManager => HttpContext.GetOwinContext().Get<SignInManager<AppIdentityUser, string>>();
        private UserManager<AppIdentityUser> _userManager => HttpContext.GetOwinContext().Get<UserManager<AppIdentityUser>>();

        private IAppRepository _appRepo;

        public AccountController(IAppRepository appRepo)
        {
            _appRepo = appRepo;
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
            if(user == null)
            {
                ModelState.AddModelError("", "Invalid Credentials.");
            }
            else if (!user.EmailConfirmed)
            {
                model.needsEmailConfirmed = true;
                ViewBag.UserID = user.Id;
                ModelState.AddModelError("EmailConfirmation", "Email has not been confirmed.");
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
                    if (user.EmailConfirmed)
                    {
                        ViewBag.UserID = user.Id;
                        model.suggestPasswordReset = true;
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            _authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult PasswordResetInit(string userID)
        {
            ViewBag.UserID = userID;
            return View();
        }

        [HttpPost]
        [ActionName("PasswordResetInit")]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> PasswordResetInit_Post(string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var resetUrl = Url.Action("ResetPassword", "Account", new { userid = user.Id, token = token }, Request.Url.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Lunch Generator App - Password Reset Requested", $"Click <a href='{resetUrl}'>here</a> to reset your password.");
                return RedirectToAction("Login", new { isPasswordRedirect = true });
            }

            return View();
        }

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
                
                var user = new AppIdentityUser(model.Email) { Email = model.Email,
                                                              FirstName = model.FirstName,
                                                              LastName = model.LastName };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user.Id, "AppLunch_User");
                    await _appRepo.CreateMemberAsync(new Member() { IdentityID = user.Id, FirstName = model.FirstName, LastName = model.LastName });

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
        public ActionResult ResetPassword(string userid, string token)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var status = await _userManager.ResetPasswordAsync(model.UserID, model.Token, model.NewPassword);
                if (status.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", status.Errors.First());
                }
            }
            return View(model);
        }
    }
}