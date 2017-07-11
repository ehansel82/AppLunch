using AppLunch.DataAccess;
using AppLunch.Interfaces;
using AppLunch.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppLunch.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager _authManager => HttpContext.GetOwinContext().Authentication;
        private RoleManager<IdentityRole> _roleManager => HttpContext.GetOwinContext().Get<RoleManager<IdentityRole>>();
        private SignInManager<AppIdentityUser, string> _signInManager => HttpContext.GetOwinContext().Get<SignInManager<AppIdentityUser, string>>();
        private UserManager<AppIdentityUser> _userManager => HttpContext.GetOwinContext().Get<UserManager<AppIdentityUser>>();

        private IAppRepository _appRepo;
        private IEmailService _emailService;

        public AccountController(IAppRepository appRepo,
                                 IEmailService emailService)
        {
            _appRepo = appRepo;
            _emailService = emailService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
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
        [AllowAnonymous]
        public ActionResult Logout()
        {
            _authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult PasswordResetInit(string userID)
        {
            ViewBag.UserID = userID;
            return View();
        }

        [HttpPost]
        [ActionName("PasswordResetInit")]
        [ValidateAntiForgeryToken()]
        [AllowAnonymous]
        public async Task<ActionResult> PasswordResetInit_Post(string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var resetUrl = Url.Action("ResetPassword", "Account", new { userid = user.Id, token = token }, Request.Url.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Lunch Generator - Password Reset Requested", $"Click <a href='{resetUrl}'>here</a> to reset your password.");
                return RedirectToAction("Login", new { isPasswordRedirect = true });
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register(Guid inviteToken, string email)
        {
            var model = new RegistrationModel() { Email = email, InviteToken = inviteToken };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var invite = await _appRepo.GetInviteByIDAsync(model.InviteToken);

                if (invite == null || model.Email != invite.InviteeEmail)
                {
                    return new HttpStatusCodeResult(403);
                }

                var user = new AppIdentityUser(model.Email)
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user.Id, "AppLunch_User");
                    await _appRepo.CreateMemberAsync(new Member() { IdentityID = user.Id, FirstName = model.FirstName, LastName = model.LastName });

                    //Since we are invite only (atm) automatically confirm the email.
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    await _userManager.ConfirmEmailAsync(user.Id, token);

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
        [AllowAnonymous]
        public ActionResult ResetPassword(string userid, string token)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AllowAnonymous]
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

        [HttpGet]
        [Authorize(Roles = "AppLunch_Manager")]
        public ActionResult Invite()
        {
            var model = new InviteModel() { Inviter = HttpContext.User.Identity.Name };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "AppLunch_Manager")]
        public async Task<ActionResult> Invite(InviteModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByName(model.Inviter);
                if (user != null)
                {
                    var member = await _appRepo.GetMemberByIdentityIdAsync(user.Id);
                    var invite = await _appRepo.InsertInviteAsync(new Invite()
                    {
                        InviteeEmail = model.Invitee,
                        Inviter = user.UserName,
                        CreateDate = DateTime.Now
                    });

                    string subject = "Lunch Generator - Registration Invite";
                    string msg = $"You have been invited by {member.FirstName} {member.LastName} to join the lunch generator community.";
                    msg += Environment.NewLine;
                    msg += $"Click <a href='{Url.Action("Register", "Account", new { inviteToken = invite.ID, email = invite.InviteeEmail }, Request.Url.Scheme)}'>here</a> to register your account.";
                    await _emailService.SendEmail(invite.InviteeEmail, subject, msg);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
    }
}