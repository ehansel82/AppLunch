using AppLunch.DataAccess;
using AppLunch.Interfaces;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AppLunch.Controllers
{
    [Authorize(Roles = "AppLunch_Admin")]
    public class LocationsController : Controller
    {
        private IAppRepository _appRepo;

        public LocationsController(IAppRepository appRepo)
        {
            _appRepo = appRepo;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var locations = await _appRepo.GetLocationsAsync();

            return View(locations);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new Location();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Location model)
        {
            model.CreateBy = HttpContext.User.Identity.Name;
            ModelState["CreateBy"].Errors.Clear();
            model.CreateDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                var location = await _appRepo.InsertLocationAsync(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}