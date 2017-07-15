using AppLunch.DataAccess;
using AppLunch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppLunch.Controllers
{
    [Authorize(Roles ="AppLunch_Manager")]
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
            ViewBag.LocationID = locationID;
            ViewBag.WorkLocations = new List<SelectListItem>() { new SelectListItem() { Text = "--select--", Value = "-1" } };
            var dbLocations = (await _repo.GetLocationsAsync()).Select(x => new SelectListItem() { Text = x.Name, Value = x.ID.ToString() }).ToList();
            ViewBag.WorkLocations = (ViewBag.WorkLocations as List<SelectListItem>).Concat(dbLocations);

            var venues = new List<Venue>();
            if (locationID < 0 || locationID == null)
            {
                ViewBag.LocationID = null;
            }
            else
            {
                venues = await _repo.GetVenuesByLocationIdAsync((int) locationID);
            }
            return View(venues);
        }

        // GET: Venues/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Venues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
