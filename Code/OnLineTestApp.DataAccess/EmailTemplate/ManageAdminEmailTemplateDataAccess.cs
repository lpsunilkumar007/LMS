using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.DataAccess.EmailTemplate
{
    public class ManageAdminEmailTemplateDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCompanyId"></param>
        /// <returns></returns>
        public List<Domain.Email.EmailTemplates> GetCompanyAssignedEmailTemplates(Guid userCompanyId)
        {
            return _DbContext.EmailTemplates.Where(x => x.IsDeleted == false &&
             x.FkAssignedCompanyId == userCompanyId).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateId"></param>
        /// <param name="userCompanyId"></param>
        /// <returns></returns>
        public Domain.Email.EmailTemplates GetEmailTemplateDetalsById(Guid emailTemplateId, Guid userCompanyId)
        {
            return _DbContext.EmailTemplates.Where(x => x.IsDeleted == false && x.FkAssignedCompanyId ==
            userCompanyId && x.EmailTemplateId == emailTemplateId).Single();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplate"></param>
        /// <param name="userCompanyId"></param>
        /// <returns></returns>
        public string UpdateEmailTemplateDetails(Domain.Email.EmailTemplates emailTemplate, Guid userCompanyId)
        {
            if (_DbContext.EmailTemplates.Where(x => x.FkAssignedCompanyId == userCompanyId && x.EmailTemplateId != emailTemplate.EmailTemplateId
             && x.EmailTemplateName == emailTemplate.EmailTemplateName
            ).Any())
            {
                return "Template with same name already exists.";
            }
            var originalData = _DbContext.EmailTemplates.Where(x => x.EmailTemplateId ==
            emailTemplate.EmailTemplateId && x.IsDeleted == false).Single();

            originalData.EmailTemplateName = emailTemplate.EmailTemplateName;
            originalData.EmailSubject = emailTemplate.EmailSubject;
            originalData.EmailFromName = emailTemplate.EmailFromName;
            originalData.EmailFromEmailAddress = emailTemplate.EmailFromEmailAddress;
            originalData.ReplyToName = emailTemplate.ReplyToName;
            originalData.ReplyToEmailAddress = emailTemplate.ReplyToEmailAddress;
            originalData.EmailTemplateDescription = emailTemplate.EmailTemplateDescription;
            originalData.EmailBody = emailTemplate.EmailBody;
            

            _DbContext.Entry(originalData).State = System.Data.Entity.EntityState.Modified;
            _DbContext.SaveChanges(createLog: true);
            return string.Empty;
        }
    }
}
