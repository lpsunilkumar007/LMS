using System.Threading.Tasks;
using System.Web.Mvc;
using OnlineTestApp.DomainLogic.Admin.MyAccount;

namespace OnlineTestApp.UI.Controllers.MyAccount
{
    public class MyAccountController : BaseClasses.AuthoriseUserControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditProfile()
        {
            MyAccountDomainLogic obj = new MyAccountDomainLogic();
            return View(obj.GetUserDetails());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult EditProfile(Domain.User.ApplicationUsers applicationUser)
        {

            //if (ModelState.IsValid)
            //{
            MyAccountDomainLogic obj = new MyAccountDomainLogic();
            obj.UpdateUserProfileDetails(applicationUser);
            return ReturnAjaxSuccessMessage("Profile details updated succesfully");
            //}
            //return ReturnAjaxModelError();           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View(new ViewModel.MyAccount.ChangePasswordViewModel());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> ChangePassword(ViewModel.MyAccount.ChangePasswordViewModel changePassword)
        {
            if (ModelState.IsValid)
            {
                MyAccountDomainLogic obj = new MyAccountDomainLogic();
                if (await obj.ChangeUserPassword(changePassword))
                {
                    return ReturnAjaxSuccessMessage("Password changed successfully");
                }
                else
                {
                    return ReturnAjaxErrorMessage("Old password does not match, please try again");
                }
            }
           return ReturnAjaxModelError();
        }
    }
}