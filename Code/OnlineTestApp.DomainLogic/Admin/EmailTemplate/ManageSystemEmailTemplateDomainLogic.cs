using System;
using System.Linq;
using System.Collections.Generic;
using OnlineTestApp.DataAccess.EmailTemplate;
using OnlineTestApp.ViewModel.Email;
using System.Threading.Tasks;
using OnlineTestApp.Domain.Company;
using OnlineTestApp.ViewModel.Company;
using OnlineTestApp.DomainLogic.Admin.BaseClasses;
using OnlineTestApp.DomainLogic.Admin.Common;

namespace OnlineTestApp.DomainLogic.Admin.EmailTemplate
{
    public class ManageSystemEmailTemplateDomainLogic : DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplate"></param>
        /// <returns></returns>
        public string AddNewEmailTemplate(Domain.Email.EmailTemplates emailTemplate)
        {
            //if (!emailTemplate.EmailTemplateCode.HasValue && !emailTemplate.FkAssignedCompanyId.HasValue)
            //{
            //    return "Please select at template least code or company";
            //}
            if (!emailTemplate.EmailTemplateCode.HasValue)
            {
                return "Please select template code";
            }
            using (ManageSystemEmailTemplateDataAccess obj = new ManageSystemEmailTemplateDataAccess())
            {
                emailTemplate.FkCreatedBy = emailTemplate.LoggedInUserId;
                emailTemplate.IsSystemTemplate = true;
                return obj.AddNewEmailTemplate(emailTemplate);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ViewSystemEmailTemplate> GetSystemEmailTemplates(ViewSystemEmailTemplate viewSystemEmailTemplate)
        {
            using (ManageSystemEmailTemplateDataAccess obj = new ManageSystemEmailTemplateDataAccess())
            {
                return await obj.GetSystemEmailTemplates(viewSystemEmailTemplate);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateId"></param>
        /// <returns></returns>
        public Domain.Email.EmailTemplates GetEmailTemplateDetalsById(Guid emailTemplateId)
        {
            using (ManageSystemEmailTemplateDataAccess obj = new ManageSystemEmailTemplateDataAccess())
            {
                return obj.GetEmailTemplateDetalsById(emailTemplateId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplate"></param>
        /// <returns></returns>
        public string UpdateEmailTemplateDetails(Domain.Email.EmailTemplates emailTemplate)
        {
            //if (!emailTemplate.EmailTemplateCode.HasValue && !emailTemplate.FkAssignedCompanyId.HasValue)
            //{
            //    return "Please select at least template code or company";
            //}
            if (!emailTemplate.EmailTemplateCode.HasValue)
            {
                return "Please select template code";
            }
            using (ManageSystemEmailTemplateDataAccess obj = new ManageSystemEmailTemplateDataAccess())
            {
                return obj.UpdateEmailTemplateDetails(emailTemplate);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateId"></param>
        public void DeleteEmailTemplate(Guid emailTemplateId)
        {
            using (ManageSystemEmailTemplateDataAccess obj = new ManageSystemEmailTemplateDataAccess())
            {
                obj.DeleteEmailTemplate(emailTemplateId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateId"></param>
        /// <returns></returns>
        public async Task<AssignSystemEmailTemplateToCompany> GetCompaniesToAssignEmailTemplates(Guid emailTemplateId)
        {
            using (ManageSystemEmailTemplateDataAccess obj = new ManageSystemEmailTemplateDataAccess())
            {
                var emailTemplate = obj.GetEmailTemplateDetalsById(emailTemplateId);
                var lstCompanies = await CompanyDomainLogic.GetAllCompanies();
                var assignedToCompanies = obj.GetAssignedCompanyIds(emailTemplate.EmailTemplateCode.Value);

                AssignSystemEmailTemplateToCompany AssignSystemEmailTemplateToCompany = new AssignSystemEmailTemplateToCompany
                {
                    EmailTemplateId = emailTemplateId,
                    LstCompany = lstCompanies.Where(x => !assignedToCompanies.Contains(x.CompanyId)).ToList()
                };
                return AssignSystemEmailTemplateToCompany;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateId"></param>
        /// <param name="lstCompanies"></param>
        public void AssignEmailTemplateToCompany(AssignSystemEmailTemplateToCompany assignSystemEmailTemplateToCompany)
        {
            using (ManageSystemEmailTemplateDataAccess obj = new ManageSystemEmailTemplateDataAccess())
            {
                obj.AssignEmailTemplateToCompany(assignSystemEmailTemplateToCompany);
            }
        }
    }
}
