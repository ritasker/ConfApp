using System.Web.Mvc;

namespace ConfApp.Web
{
    public class ValidatorActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData.ModelState.IsValid) return;

            filterContext.Result = new ViewResult
            {
                ViewName =$"~/Views{filterContext.RequestContext.HttpContext.Request.Path}.cshtml",
                ViewData = filterContext.Controller.ViewData
            };
        }
    }
}