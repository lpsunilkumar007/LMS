using Company.OnlineTestApp.UI.Controllers.Base;
using OnlineTestApp;
using OnlineTestApp.Domain.User;
using OnlineTestApp.DomainLogic.Company;
using OnlineTestApp.ViewModel.UserMemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Company.OnlineTestApp.UI.Controllers
{
    public class AccountController : AnonymousController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>       
        [HttpGet]
        public ActionResult SendEmail()
        {
            var msg = "";
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.To.Add(Utilities.AppSettings.GetStringValue("ToEmail"));
            mail.From = new MailAddress(Utilities.AppSettings.GetStringValue("EmailFrom"));
            mail.Subject = "test";
            mail.IsBodyHtml = true;
            mail.Body = "test";
            //SmtpServer.Port = 587;
            //SmtpServer.Host = "smtp.gmail.com";
            //SmtpServer.Credentials = new System.Net.NetworkCredential("test.essorsolutions@gmail.com", "test123#");
            //SmtpServer.EnableSsl = true;
            SmtpServer.Host = Utilities.AppSettings.GetStringValue("Host");
            //"smtpout.secureserver.net";
            SmtpServer.Port = Convert.ToInt32(Utilities.AppSettings.GetStringValue("Port"));
            SmtpServer.Credentials = new System.Net.NetworkCredential(Utilities.AppSettings.GetStringValue("NetworkCredential_UserName"), Utilities.AppSettings.GetStringValue("NetworkCredential_Password"));
            SmtpServer.EnableSsl = Convert.ToBoolean(Utilities.AppSettings.GetStringValue("EnableSsl"));
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.
                Network;

            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                msg = "My Message " + ex.Message + ex.InnerException;
            }
            ViewBag.Messsage = msg;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            CompanyUserDomainLogic userLoginDomainLogic = new CompanyUserDomainLogic();
            return UserVariables.IsAuthenticated ? RedirectAfterLogin(returnUrl) :
            View(userLoginDomainLogic.GetRemeberMeUserDetails());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CompanyUserViewModel userLogin, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                CompanyUserDomainLogic userLoginDomainLogic = new CompanyUserDomainLogic();
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public async Task<JsonResult> ValidateUserByEmail(string email)
        {
            if (!Request.IsAjaxRequest()) { return null; }
            CompanyUserDomainLogic companyUserDomainLogic = new CompanyUserDomainLogic();
            ApplicationUsers userInfo = companyUserDomainLogic.CheckValidUserByEmail(email);
            if (userInfo == null)
            {
                return Json_GetValue(userInfo);
            }
            else
            {
                await companyUserDomainLogic.SendPasswordToUser(userInfo);
                //await companyUserDomainLogic.UpdateUserPassword(userInfo);
            }
            return Json_AllowGet_IgnoreReferenceLoop(userInfo);
        }


    }
}