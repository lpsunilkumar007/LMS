
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace OnlineTestApp.DataAccess.Common
{
    public class CompanyDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Domain.Company.Companies>> GetAllCompanies()
        {
            return await _DbContext.Company.Where(x => x.CanDisplayCompany == true && x.IsDeleted == false).OrderBy(x => x.CompanyName).ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<Domain.Company.Companies> GetCompanyDetailsById(Guid companyId)
        {
            return await _DbContext.Company.Where(x => x.IsDeleted == false && x.CompanyId==companyId).SingleAsync();
        }
    }
}
