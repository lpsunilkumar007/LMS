using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System;

namespace OnlineTestApp.DataAccess.Common
{
    public  class EmailDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplateCode"></param>
        /// <returns></returns>
        public async Task<Domain.Email.EmailTemplates> GetEmailTemplateDetailsAssignedToCompany(Enums.Email.EmailTemplateCode emailTemplateCode,Guid fkCompanyId)
        {
            return await _DbContext.EmailTemplates.Where(x => x.EmailTemplateCode == emailTemplateCode && x.IsDeleted == false && x.FkAssignedCompanyId== fkCompanyId).SingleOrDefaultAsync();
        }
    }
}
