using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace WorkFollow.Web.Filters;

public class CustomAuthorizeAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var userId = filterContext.HttpContext.Session.GetInt32("kullaniciid");
        if (userId == null && !userId.HasValue)
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "action", "Login" },
                    { "controller", "Account" }
                });
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        // var user = filterContext.HttpContext.Session.Get<User>("User");
    }
}