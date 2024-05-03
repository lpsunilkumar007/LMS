using OnlineTestApp.ViewModel.Email;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OnlineTestApp.Domain.Company;
using System.Collections.Generic;
using OnlineTestApp.Domain.Email;

namespace OnlineTestApp.DataAccess.EmailTemplate
{
    public class ManageSystemEmailTemplateDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplate"></param>
        /// <returns></returns>
        public string AddNewEmailTemplate(Domain.Email.EmailTemplates emailTemplate)
        {
            var queryExists = _DbContext.EmailTemplates.Where(x => x.IsSystemTemplate == true);

            if (queryExists.Where(x => x.EmailTemplateCode == emailTemplate.EmailTemplateCode
             ).Any())
                return "Email template already exists for selected code";


            _DbContext.EmailTemplates.Add(emailTemplate);
            _DbContext.SaveChanges(createLog: false);
            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ViewSystemEmailTemplate> GetSystemEmailTemplates(ViewSystemEmailTemplate viewSystemEmailTemplate)
        {
            var query = _DbContext.EmailTemplates.Where(x => x.IsDeleted == false && x.IsSystemTemplate == true);
            viewSystemEmailTemplate.LstEmailTemplates = await query.ToListAsync();
            return viewSystemEmailTemplate;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggedInUserId"></param>
        /// <param name="emailTemplateId"></param>
        /// <returns></returns>
        public Domain.Email.EmailTemplates GetEmailTemplateDetalsById(Guid emailTemplateId)
        {
            return _DbContext.EmailTemplates.Where(x => x.IsDeleted == false && x.IsSystemTemplate ==
            true && x.EmailTemplateId == emailTemplateId).Single();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplate"></param>
        /// <returns></returns>
        public string UpdateEmailTemplateDetails(Domain.Email.EmailTemplates emailTemplate)
        {
            if (_DbContext.EmailTemplates.
            Where(x => x.IsSystemTemplate == true && x.EmailTemplateId != emailTemplate.EmailTemplateId && x.EmailTemplateCode == emailTemplate.EmailTemplateCode)
            .Any())
                return "Email template already exists for selected code";

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
            originalData.EmailTemplateCode = emailTemplate.EmailTemplateCode;

            _DbContext.Entry(originalData).State = System.Data.Entity.EntityState.Modified;
            _DbContext.SaveChanges(createLog: true);
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateId"></param>
        /// <param name="lstCompanies"></param>
        public void AssignEmailTemplateToCompany(AssignSystemEmailTemplateToCompany assignSystemEmailTemplateToCompany)
        {
            var originalData = _DbContext.EmailTemplates.Where(x => x.EmailTemplateId == assignSystemEmailTemplateToCompany.EmailTemplateId && x.IsDeleted == false).Single();
            foreach (var lst in assignSystemEmailTemplateToCompany.LstCompanyIds)
            {
                EmailTemplates emailTemplate = new EmailTemplates
                {
                    EmailTemplateName = originalData.EmailTemplateName,
                    EmailSubject = originalData.EmailSubject,
                    EmailFromName = originalData.EmailFromName,
                    EmailFromEmailAddress = originalData.EmailFromEmailAddress,
                    ReplyToName = originalData.ReplyToName,
                    ReplyToEmailAddress = originalData.ReplyToEmailAddress,
                    EmailTemplateDescription = originalData.EmailTemplateDescription,
                    EmailBody = originalData.EmailBody,
                    FkAssignedCompanyId = lst,
                    EmailTemplateCode = originalData.EmailTemplateCode,
                    //IsSystemTemplate = false,
                    //IsSharedTemplate = originalData.IsSharedTemplate,
                    FkCreatedBy = UserVariables.LoggedInUserId
                };
                _DbContext.EmailTemplates.Add(emailTemplate);
            }
            _DbContext.SaveChanges(createLog: false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateId"></param>
        /// <returns></returns>
        public Guid[] GetAssignedCompanyIds(Enums.Email.EmailTemplateCode emailTemplateCode)
        {
            return _DbContext.EmailTemplates.Where(x => x.FkAssignedCompanyId != null && x.IsDeleted == false).Select(x => x.FkAssignedCompanyId.Value).ToArray();
            //List<Companies> lstCompanies = new List<Companies>();
            //var query = _DbContext.EmailTemplates
            //    //.Include(x => x.Company)
            //    .Where(x => x.IsDeleted == false && x.IsSystemTemplate == false && x.EmailTemplateCode == emailTemplateCode && x.FkAssignedCompanyId.HasValue)
            //    .Select(x => x.FkAssignedCompanyId.Value).ToList();
            ////if (query.Count > 0)
            //return _DbContext.Company.Where(y => !query.Contains(y.CompanyId) && y.IsDeleted == false).ToList();
            //// else
            ////lstCompanies = _DbContext.Company.Where(y => y.IsDeleted == false).ToList();
            //// return lstCompanies;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateId"></param>
        public void DeleteEmailTemplate(Guid emailTemplateId)
        {
            var originalData = _DbContext.EmailTemplates.Where(x => x.EmailTemplateId ==
            emailTemplateId && x.IsDeleted == false && x.IsSystemTemplate == true).Single();
            originalData.IsDeleted = true;
            originalData.DeletedDateDateTime = DateSettings.CurrentDateTime;
            originalData.EmailTemplateCode = null;
            originalData.FkAssignedCompanyId = null;
            originalData.EmailTemplateName = emailTemplateId + "--" + originalData.EmailTemplateName;
            _DbContext.Entry(originalData).State = System.Data.Entity.EntityState.Modified;
            _DbContext.SaveChanges(createLog: true);
        }
    }
}
