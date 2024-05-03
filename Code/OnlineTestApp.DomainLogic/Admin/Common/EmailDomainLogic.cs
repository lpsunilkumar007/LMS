using OnlineTestApp.DataAccess.Common;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.Common
{
    public static class EmailDomainLogic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateCode"></param>
        /// <returns></returns>
        public static async Task<Domain.Email.SendEmail> GetEmailTemplateDetailsAssignedToCompany(Enums.Email.EmailTemplateCode emailTemplateCode)
        {
            using (EmailDataAccess obj = new EmailDataAccess())
            {
                var result = await obj.GetEmailTemplateDetailsAssignedToCompany(emailTemplateCode, UserVariables.UserCompanyId);
                return new Domain.Email.SendEmail(result ?? new Domain.Email.EmailTemplates());
            }
        }
    }
}
