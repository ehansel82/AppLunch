using System.Web.Mvc;

namespace AppLunch.Controllers
{
    [Authorize(Roles = "AppLunch_User")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}