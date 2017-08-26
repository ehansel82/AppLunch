using AppLunch.DataAccess;
using AppLunch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AppLunch.Controllers
{
    [Authorize(Roles = "AppLunch_Manager")]
    public class VenuesController : Controller
    {
        private IAppRepository _repo;

        public VenuesController(IAppRepository repo)
        {
            _repo = repo;
        }

        // GET: Venues
        public async Task<ActionResult> Index(int? locationID)
        {
            ViewBag.WorkLocations = (await _repo.GetLocationsAsync()).Select(x => new SelectListItem() { Text = x.Name, Value = x.ID.ToString() }).ToList();

            var venues = new List<Venue>();
            if (locationID < 0 || locationID == null)
            {
                ViewBag.LocationID = null;
            }
            else
            {
                venues = await _repo.GetVenuesByLocationIdAsync((int)locationID);
            }
            return View(venues);
        }

        // GET: Venues/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetByWorkLocation(int locationID)
        {
            List<Venue> venues = await _repo.GetVenuesByLocationIdAsync(locationID);
            return new JsonResult() { Data = venues, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Venues/Create
        public ActionResult Create(int? locationID)
        {
            if (locationID == null)
            {
                return new HttpStatusCodeResult(400);
            }

            ViewBag.LocationID = locationID;
            return View();
        }

        // POST: Venues/Create
        [HttpPost]
        public async Task<ActionResult> Create(Venue model, int locationID)
        {
            model.CreateBy = HttpContext.User.Identity.Name;
            ModelState["CreateBy"].Errors.Clear();
            model.CreateDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                await _repo.CreateVenueAsync(model, locationID);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Venues/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Venues/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Venues/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Venues/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}