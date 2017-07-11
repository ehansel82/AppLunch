using AppLunch.DataAccess;
using AppLunch.Interfaces;
using AppLunch.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppLunch.Controllers
{
    [Authorize(Roles = ("AppLunch_Admin"))]
    public class AdminController : Controller
    {
        private RoleManager<IdentityRole> _roleManager => HttpContext.GetOwinContext().Get<RoleManager<IdentityRole>>();
        private UserManager<AppIdentityUser> _userManager => HttpContext.GetOwinContext().Get<UserManager<AppIdentityUser>>();
        private IAppRepository _appRepo;
        private IMapper _mapper;

        public AdminController(IAppRepository appRepo,
                               IMapper mapper)
        {
            _appRepo = appRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> ManageUsers()
        {
            var users = (await _appRepo.GetMembersAsync());

            var model = _mapper.Map<List<ManageUsersModel>>(users);

            foreach (var rec in model)
            {
                rec.IsManager = _userManager.GetRoles(rec.IdentityID).ToList().Exists(x => x == "AppLunch_Manager");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<HttpStatusCodeResult> SetToManager(string identityID)
        {
            var user = await _userManager.FindByIdAsync(identityID);
            await _userManager.AddToRoleAsync(user.Id, "AppLunch_Manager");

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public async Task<HttpStatusCodeResult> RevokeManager(string identityID)
        {
            var user = await _userManager.FindByIdAsync(identityID);
            await _userManager.RemoveFromRoleAsync(user.Id, "AppLunch_Manager");

            return new HttpStatusCodeResult(200);
        }
    }
}