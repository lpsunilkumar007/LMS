using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using OnlineTestApp.Domain.Email;
using OnlineTestApp.DomainLogic.Admin.EmailTemplate;
using OnlineTestApp.ViewModel.Email;
using System.Collections.Generic;
using OnlineTestApp.ViewModel.Company;
using OnlineTestApp.DomainLogic.Admin.Common;

namespace OnlineTestApp.UI.Controllers.EmailTemplate
{
    public class ManageSystemEmailTemplateController : BaseClasses.SuperAdminUserControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ViewEmailTemplates(ViewSystemEmailTemplate viewSystemEmailTemplate)
        {
            ManageSystemEmailTemplateDomainLogic obj = new ManageSystemEmailTemplateDomainLogic();
            //viewSystemEmailTemplate.LstCompany = await DomainLogic.Common.CompanyDomainLogic.GetAllCompanies();
            viewSystemEmailTemplate = await obj.GetSystemEmailTemplates(viewSystemEmailTemplate);

            return View(viewSystemEmailTemplate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AddNewEmailTemplate()
        {
            return View(await InitializeAddNewEmailTemplate(null));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplates"></param>
        /// <returns></returns>
        async Task<AddEditSystemEmailTemplate> InitializeAddNewEmailTemplate(AddEditSystemEmailTemplate emailTemplates)
        {
            if (emailTemplates == null)
            {
                emailTemplates = new AddEditSystemEmailTemplate();
                var userDetails = await UserDomainLogic.GetUserDetailsAsync();
                emailTemplates.EmailTemplates.ReplyToName = userDetails.FullName;
                emailTemplates.EmailTemplates.ReplyToEmailAddress = userDetails.EmailAddress;
            }
            emailTemplates.LstCompany = await CompanyDomainLogic.GetAllCompanies();
            return emailTemplates;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplate"></param>
        /// <returns></returns>
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> AddNewEmailTemplate(AddEditSystemEmailTemplate emailTemplate)
        {
            if (ModelState.IsValid)
            {
                ManageSystemEmailTemplateDomainLogic obj = new ManageSystemEmailTemplateDomainLogic();
                string result = obj.AddNewEmailTemplate(emailTemplate.EmailTemplates);
                if (string.IsNullOrEmpty(result))
                {
                    Success("Email template created successfully");
                    emailTemplate = new AddEditSystemEmailTemplate();
                    ModelState.Clear();
                }
                else
                {
                    ErrorBlock(result);
                }
            }
            return View(await InitializeAddNewEmailTemplate(emailTemplate));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> EditEmailTemplate(Guid emailTemplateId)
        {
            ManageSystemEmailTemplateDomainLogic obj = new ManageSystemEmailTemplateDomainLogic();
            AddEditSystemEmailTemplate result = new AddEditSystemEmailTemplate
            {
                EmailTemplates = obj.GetEmailTemplateDetalsById(emailTemplateId)
            };
            return View(await InitializeAddNewEmailTemplate(result));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplate"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> EditEmailTemplate(AddEditSystemEmailTemplate emailTemplate, Guid emailTemplateId)
        {
            emailTemplate.EmailTemplates.EmailTemplateId = emailTemplateId;
            if (ModelState.IsValid)
            {
                ManageSystemEmailTemplateDomainLogic obj = new ManageSystemEmailTemplateDomainLogic();
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
            return View(await InitializeAddNewEmailTemplate(emailTemplate));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateId"></param>
        [HttpPost]
        public void DeleteEmailTemplate(Guid emailTemplateId)
        {
            if (!Request.IsAjaxRequest()) return;
            ManageSystemEmailTemplateDomainLogic obj = new ManageSystemEmailTemplateDomainLogic();
            obj.DeleteEmailTemplate(emailTemplateId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AssignEmailTemplateToCompany(Guid emailTemplateId)
        {
            ManageSystemEmailTemplateDomainLogic obj = new ManageSystemEmailTemplateDomainLogic();
            return View(await obj.GetCompaniesToAssignEmailTemplates(emailTemplateId));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignSystemEmailTemplateToCompany"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult AssignEmailTemplateToCompany(AssignSystemEmailTemplateToCompany assignSystemEmailTemplateToCompany)
        {
            if (ModelState.IsValid)
            {
                ManageSystemEmailTemplateDomainLogic obj = new ManageSystemEmailTemplateDomainLogic();
                obj.AssignEmailTemplateToCompany(assignSystemEmailTemplateToCompany);
                return ReturnAjaxSuccessMessage("Company Assigned sucessfully");
            }
            return ReturnAjaxModelError();
        }

    }
}