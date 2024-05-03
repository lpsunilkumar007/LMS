using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.OnlineTestApp.UI.Controllers.Base
{
    public class AnonymousController : BaseController
    {

        protected ActionResult RedirectAfterLogin(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("index", "dashboard");
            }
        }

    }
}