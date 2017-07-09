using System.Web.Mvc;

namespace AppLunch.Filters
{
    public class ViewBagGlobalAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.LoggedInUserName = filterContext.HttpContext.User.Identity.Name;
            filterContext.Controller.ViewBag.IsAdmin = filterContext.HttpContext.User.IsInRole("AppLunch_Admin");
            filterContext.Controller.ViewBag.IsManager = filterContext.HttpContext.User.IsInRole("AppLunch_Manager");

            filterContext.Controller.ViewBag.IsAccountController = (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToUpper() == "ACCOUNT");
        }
    }
}