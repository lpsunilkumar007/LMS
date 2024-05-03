using OnlineTestApp.DataAccess.Common;
using OnlineTestApp.DataAccess.Company;
using OnlineTestApp.Domain.Email;
using OnlineTestApp.Domain.User;
using OnlineTestApp.DomainLogic.Admin.BaseClasses;
using OnlineTestApp.Enums.Email;
using OnlineTestApp.ViewModel.UserMemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Utilities.EmailSender;
using Utilities.EmailSender.Domain;

namespace OnlineTestApp.DomainLogic.Company
{
    public class CompanyUserDomainLogic : DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CompanyUserViewModel GetRemeberMeUserDetails()
        {
            CompanyUserViewModel userLoginViewModel = new CompanyUserViewModel();

            string rMCEmail = GetValueFromCookie("rMCEmail");
            string rMCUserPass = GetValueFromCookie("rMCUserPass");
            if (!String.IsNullOrEmpty(rMCEmail) && !String.IsNullOrEmpty(rMCUserPass))
            {
                userLoginViewModel.EmailAddress = rMCEmail;
                userLoginViewModel.UserPassword = DecryptText(rMCUserPass, userLoginViewModel.EmailAddress);
                userLoginViewModel.RememberMe = true;
            }
            return userLoginViewModel;
        }


        /// <summary>
        /// send password in email 
        /// </summary>
        /// <param name="applicationUsers"></param>
        /// <returns></returns>
        public async Task SendPasswordToUser(ApplicationUsers applicationUsers)
        {
            string decriptedPassword = DecryptText(applicationUsers.UserPassword, applicationUsers.EmailAddress);
            //InviteTestDataAccess inviteTestDataAccess = new InviteTestDataAccess();
            EmailDomain emailDomain = new EmailDomain();
            EmailDataAccess emailDataAccess = new EmailDataAccess();
            EmailTemplates emailTemplates = await emailDataAccess.GetEmailTemplateDetailsAssignedToCompany(EmailTemplateCode.ForgetPassword, applicationUsers.FkCompanyId);

            emailDomain.EmailTo = applicationUsers.EmailAddress;
            emailDomain.EmailFrom = //Utilities.AppSettings.GetStringValue("EmailFrom");// "sunil.kumar@essorsolutions.net";
            emailTemplates.EmailFromEmailAddress;
            emailDomain.EmailSubject = emailTemplates.EmailSubject;
            string emailBody = emailTemplates.EmailBody.Replace("new-password", decriptedPassword);
            //"Your New Password is :" + applicationUsers.UserPassword;
            emailDomain.EmailBody = emailBody;
            emailDomain.IsBodyHtml = true;
            await EmailSender.SendEmail(emailDomain);
        }
        // <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        /// 

        public string UserLogin(CompanyUserViewModel userLogin)
        {
            using (CompanyUserDataAccess companyUserDataAccess = new CompanyUserDataAccess())
            {
                userLogin.UserPassword = EncryptText(userLogin.UserPassword, userLogin.EmailAddress);
                var result = companyUserDataAccess.UserLogin(userLogin);
                if (result == null) return "Please enter correct email and password";
                if (!result.IsActive) return "Your account is suspended. Please contact administrator";
                if (result.IsDeleted) return "Your accoint is deleted from our records. Please contact administrator";
                //create ticket
                bool rememberMe = userLogin.RememberMe;
                RememberMe(rememberMe, result);
                AuthencationTicket(result);
                return "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        internal void AuthencationTicket(ApplicationUsers applicationUsers)
        {
            bool isSuperAdmin = applicationUsers.ApplicationUserRoles.UserRoleId == Enums.User.UserRoles.SuperAdmin.ToType<short>();
            string userData =
                applicationUsers.ApplicationUserId + "}" //0 - Logged in user Id
            + isSuperAdmin + "}" //1 - is super admin
            + applicationUsers.IsSystemUser + "}" //2
            + applicationUsers.FkCompanyId + "}"//3 - company id
            + applicationUsers.ApplicationUserRoles.UserRoleId //4 user role id
            ;
            FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, userData, DateTime.Now, DateTime.Now.AddDays(25), false
                , "User"
                , FormsAuthentication.FormsCookiePath);
            string st = FormsAuthentication.Encrypt(tkt);
            HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, st) { HttpOnly = true };
            HttpContext.Current.Response.Cookies.Add(ck);
        }
        /// <summary>
        /// check whether entered email is exist or not 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>

        public ApplicationUsers CheckValidUserByEmail(string email)
        {
            using (CompanyUserDataAccess companyUserDataAccess = new CompanyUserDataAccess())
            {
                return companyUserDataAccess.CheckValidUserByEmail(email);
            }
        }

        /// <summary>
        /// if user select remeber me save email and pass in cookies 
        /// </summary>
        /// <param name="rememberMe"></param>
        /// <param name="applicationUsers"></param>
        internal void RememberMe(bool rememberMe, ApplicationUsers applicationUsers)
        {
            if (rememberMe)
            {
                AddCookie("rMCEmail", applicationUsers.EmailAddress, DateTime.Now.AddDays(25));
                AddCookie("rMCUserPass", applicationUsers.UserPassword, DateTime.Now.AddDays(25));
            }
            else
            {
                removeCookie("rMCEmail");
                removeCookie("rMCUserPass");
            }

        }

        /// <summary>
        /// add cookie 
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>

        internal void AddCookie(string cookieName, string value, DateTime expiry)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = value;
            cookie.Expires = expiry;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// emove / expire cookie
        /// </summary>
        /// <param name="cookieName"></param>
        internal void removeCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                HttpContext.Current.Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(-25);
            }
        }
        /// <summary>
        /// get value from saved cookie 
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        internal string GetValueFromCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                return HttpContext.Current.Request.Cookies[cookieName].Value;
            }
            return null;
        }


    }
}
