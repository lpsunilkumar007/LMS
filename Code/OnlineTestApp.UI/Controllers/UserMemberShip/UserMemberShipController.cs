using OnlineTestApp.DomainLogic.Admin.UserMemberShip;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineTestApp.UI.Controllers.UserMemberShip
{
    public class UserMemberShipController : BaseClasses.AnonymousUserControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return UserVariables.IsAuthenticated ? RedirectAfterLogin(returnUrl) :
                View(new ViewModel.UserMemberShip.UserLoginViewModel());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ViewModel.UserMemberShip.UserLoginViewModel userLogin, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserLoginDomainLogic userLoginDomainLogic = new UserLoginDomainLogic();
                string result = userLoginDomainLogic.UserLogin(userLogin);
                if (!string.IsNullOrEmpty(result))
                {
                    ErrorBlock(result);
                }
                else
                {
                    return RedirectAfterLogin(returnUrl);
                }
            }
            return View(userLogin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToLogin();
        }
    }
}