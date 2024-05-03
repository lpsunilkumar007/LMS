using OnlineTestApp.DomainLogic.Admin.Menu;
using OnlineTestApp.DomainLogic.Admin.User;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.Menu
{
    public class MenuController : BaseClasses.ControllerBase
    {
        [ChildActionOnly]
        public ActionResult _topMenu()
        {
            return PartialView( MenuDomainLogic.GetUserDetailsForTopMenu());
        }

        [HttpGet]
        public JsonResult GetTopMenuDetails()
        {
            if (!Request.IsAjaxRequest()) return NotAjaxRequest();
            return Json_AllowGet_IgnoreReferenceLoop(MenuDomainLogic.GetUserDetailsForTopMenu());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task ChangeUserMenuSetting()
        {
            if (!Request.IsAjaxRequest()) return;
            await ManageUserSettingsDomainLogic.ChangeLeftMenuSetting();
        }
    }
}