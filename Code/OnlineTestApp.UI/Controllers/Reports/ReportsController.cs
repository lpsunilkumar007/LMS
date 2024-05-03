using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.Reports
{
    public class ReportsController : BaseClasses.AuthoriseUserControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DashBoard()
        {
            return View();
        }
    }
}