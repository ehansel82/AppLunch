using AppLunch.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AppLunch.Controllers
{
    [Authorize(Roles = "AppLunch_Manager")]
    public class LocationController : Controller
    {
        private IAppRepository _appRepo;

        public LocationController(IAppRepository appRepo)
        {
            _appRepo = appRepo;
        }

        public async Task<ActionResult> Index()
        {
            var locations = await _appRepo.GetLocationsAsync();

            return View(locations);
        }
    }
}