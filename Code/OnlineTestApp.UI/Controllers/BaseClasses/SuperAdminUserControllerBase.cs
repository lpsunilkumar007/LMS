using System.Web.Mvc;


namespace OnlineTestApp.UI.Controllers.BaseClasses
{
    [SuperAdminUserModulesCheck]
    public class SuperAdminUserControllerBase : AuthoriseUserControllerBase
    {

    }
    class SuperAdminUserModulesCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserVariables.CanAccessEverything) return;

            //else go away
            filterContext.Result = new RedirectResult(SystemSettings.UnauthorizedPageUrl);
        }
    }
}