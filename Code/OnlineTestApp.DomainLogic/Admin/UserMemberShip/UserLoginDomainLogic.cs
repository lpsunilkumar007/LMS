using OnlineTestApp.DataAccess.UserMemberShip;
using System;
using System.Web;
using System.Web.Security;

namespace OnlineTestApp.DomainLogic.Admin.UserMemberShip
{
    public class UserLoginDomainLogic : BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public string UserLogin(ViewModel.UserMemberShip.UserLoginViewModel userLogin)
        {
            using (UserLoginDataAccess obj = new UserLoginDataAccess())
            {
                userLogin.UserPassword = EncryptText(userLogin.UserPassword, userLogin.UserName);
                var result = obj.UserLogin(userLogin);

                if (result == null) return "Please enter correct username and password";
                if (!result.IsActive) return "Your account is suspended. Please contact administrator";
                if (result.IsDeleted) return "Your accoint is deleted from our records. Please contact administrator";
                //create ticket
                AuthencationTicket(result);
                return "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        internal void AuthencationTicket(Domain.User.ApplicationUsers applicationUsers)
        {
            bool isSuperAdmin = applicationUsers.ApplicationUserRoles.UserRoleId == Enums.User.UserRoles.SuperAdmin.ToType<short>();
            
            string userData =
                applicationUsers.ApplicationUserId + "}" //0 - Logged in user Id
            + isSuperAdmin + "}" //1 - is super admin
            + applicationUsers.IsSystemUser  + "}" //2
            + applicationUsers.FkCompanyId + "}"//3 - company id
            + applicationUsers.ApplicationUserRoles.UserRoleId //4 user role id
            ;
            FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, userData, DateTime.Now, DateTime.Now.AddHours(5), false
                , "User"
                , FormsAuthentication.FormsCookiePath);
            string st = FormsAuthentication.Encrypt(tkt);
            HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, st) { HttpOnly = true };
            HttpContext.Current.Response.Cookies.Add(ck);
        }
    }
}
