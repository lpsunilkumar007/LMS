using OnlineTestApp.DataAccess.EmailTemplate;
using System;
using System.Collections.Generic;

namespace OnlineTestApp.DomainLogic.Admin.EmailTemplate
{
    public class ManageAdminEmailTemplateDomainLogic : OnlineTestApp.DomainLogic.Admin.BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Domain.Email.EmailTemplates> GetCompanyAssignedEmailTemplates()
        {
            using (ManageAdminEmailTemplateDataAccess obj = new ManageAdminEmailTemplateDataAccess())
            {
                return obj.GetCompanyAssignedEmailTemplates(UserVariables.UserCompanyId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateId"></param>
        /// <returns></returns>
        public Domain.Email.EmailTemplates GetEmailTemplateDetalsById(Guid emailTemplateId)
        {
            using (ManageAdminEmailTemplateDataAccess obj = new ManageAdminEmailTemplateDataAccess())
            {
                return obj.GetEmailTemplateDetalsById(emailTemplateId: emailTemplateId, userCompanyId: UserVariables.UserCompanyId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplate"></param>
        /// <returns></returns>
        public string UpdateEmailTemplateDetails(Domain.Email.EmailTemplates emailTemplate)
        {
            using (ManageAdminEmailTemplateDataAccess obj = new ManageAdminEmailTemplateDataAccess())
            {
                return obj.UpdateEmailTemplateDetails(emailTemplate: emailTemplate, userCompanyId: UserVariables.UserCompanyId);
            }
        }
    }
}
