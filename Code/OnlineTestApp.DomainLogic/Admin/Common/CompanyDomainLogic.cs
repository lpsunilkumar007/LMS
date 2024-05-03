using OnlineTestApp.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.Common
{
    public static class CompanyDomainLogic
    {
        public static async Task<List<Domain.Company.Companies>> GetAllCompanies()
        {
            using (CompanyDataAccess obj = new CompanyDataAccess())
            {
                return await obj.GetAllCompanies();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static async Task<Domain.Company.Companies> GetCompanyDetailsById(Guid companyId)
        {
            using (CompanyDataAccess obj = new CompanyDataAccess())
            {
                return await obj.GetCompanyDetailsById(companyId);
            }
        }
    }
}
