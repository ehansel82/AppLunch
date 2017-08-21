using AppLunch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppLunch.Controllers
{
    [Authorize(Roles = "AppLunch_User")]
    public class RankingsController : Controller
    {
        private IAppRepository _repo;

        public RankingsController(IAppRepository repo)
        {
            _repo = repo;
        }

        // GET: Rankings
        public async Task<ActionResult> Index()
        {
            ViewBag.WorkLocations = (await _repo.GetLocationsAsync()).Select(x => new SelectListItem() { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View();
        }
    }
}