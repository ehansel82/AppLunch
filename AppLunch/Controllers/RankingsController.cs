using AppLunch.DataAccess;
using AppLunch.Interfaces;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Runtime.Serialization;

namespace AppLunch.Controllers
{
    [Authorize(Roles = "AppLunch_User")]
    public class RankingsController : Controller
    {
        private UserManager<AppIdentityUser> _userManager => HttpContext.GetOwinContext().Get<UserManager<AppIdentityUser>>();
        private IAppRepository _repo;

        public RankingsController(IAppRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            ViewBag.WorkLocations = (await _repo.GetLocationsAsync()).Select(x => new SelectListItem() { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Submit(SubmitObject submission)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            await _repo.InsertOrUpdateRankingAsync(submission.venueID, submission.ranking, user.Id);

            return new HttpStatusCodeResult(200);
        }
    }

    [DataContract]
    public class SubmitObject
    {
        [DataMember]
        public int venueID { get; set; }
        [DataMember]
        public int ranking { get; set; }
    }
}