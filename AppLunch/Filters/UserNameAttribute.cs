using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace AppLunch.Filters
{
    public class UserNameAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToUpper() != "ACCOUNT")
            {
                filterContext.Controller.ViewBag.LoggedInUserName = filterContext.HttpContext.User.Identity.Name;
            }

            var userManager = filterContext.HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();

            var roles = userManager.GetRoles(filterContext.HttpContext.User.Identity.GetUserId());
        }
    }
}