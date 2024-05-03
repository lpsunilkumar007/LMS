using OnlineTestApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.OnlineTestApp.UI.Controllers.Base
{
    [CompanyUserModulesCheck]
    public class AuthorizeController : BaseController
    {

    }

    class CompanyUserModulesCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserVariables.IsAuthenticated) return;
            //else go away
            filterContext.Result = new RedirectResult(SystemSettings.LoginPageUrl);
        }
    }
}