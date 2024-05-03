using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers
{
    public class ErrorController : BaseClasses.AnonymousUserControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Error404()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}