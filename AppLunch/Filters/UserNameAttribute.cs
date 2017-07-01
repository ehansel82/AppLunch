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
        }
    }
}