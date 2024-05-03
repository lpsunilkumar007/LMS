using OnlineTestApp.DomainLogic.Admin.EmailTemplate;
using OnlineTestApp.ViewModel.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.EmailTemplate
{
    public class ManageAdminEmailTemplatesController : BaseClasses.AdminUserControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewEmailTemplates()
        {
            ManageAdminEmailTemplateDomainLogic obj = new ManageAdminEmailTemplateDomainLogic();
            return View(obj.GetCompanyAssignedEmailTemplates());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditEmailTemplate(Guid emailTemplateId)
        {
            ManageAdminEmailTemplateDomainLogic obj = new ManageAdminEmailTemplateDomainLogic();
            AddEditSystemEmailTemplate result = new AddEditSystemEmailTemplate
            {
                EmailTemplates = obj.GetEmailTemplateDetalsById(emailTemplateId)
            };
            return View(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplate"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditEmailTemplate(AddEditSystemEmailTemplate emailTemplate, Guid emailTemplateId)
        {
            emailTemplate.EmailTemplates.EmailTemplateId = emailTemplateId;
            if (ModelState.IsValid)
            {
                ManageAdminEmailTemplateDomainLogic obj = new ManageAdminEmailTemplateDomainLogic();
                string result = obj.UpdateEmailTemplateDetails(emailTemplate.EmailTemplates);
                if (string.IsNullOrEmpty(result))
                {
                    Success("Email template details updated successfully");

                }
                else
                {
                    ErrorBlock(result);
                }
            }
            return View(emailTemplate);
        }
    }
}