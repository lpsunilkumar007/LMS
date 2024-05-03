using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.BaseClasses
{
    [AdminUserModulesCheck]
    public class AdminUserControllerBase : AuthoriseUserControllerBase
    {
    }


    class AdminUserModulesCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserVariables.CanAccessEverything || UserVariables.CanAccessAdminModules) return;


            //else go away
            filterContext.Result = new RedirectResult(SystemSettings.UnauthorizedPageUrl);
        }
    }
}