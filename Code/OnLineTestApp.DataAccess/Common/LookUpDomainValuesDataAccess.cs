using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OnlineTestApp.Enums.LookUps;
using OnlineTestApp.Domain.LookUps;

namespace OnlineTestApp.DataAccess.Common
{
    public class LookUpDomainValuesDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainCode"></param>
        /// <returns></returns>
        public async Task<List<LookUpDomainValues>> GetLookUpDomainValueByLookUpCode(LookUpDomainCode lookUpDomainCode)
        {
            var result = from a in _DbContext.LookUpDomains
                         join b in _DbContext.LookUpDomainValues
                         on a.LookUpDomainId equals b.FkLookUpDomainId

                         where a.LookUpDomainCode == lookUpDomainCode
                         && b.IsDeleted == false && b.IsActive == true
                         select b
                       ;

            return await result.OrderBy(x => x.DisplayOrder).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValuesCode"></param>
        /// <returns></returns>
        public async Task<LookUpDomainValues> GetLookUpDomainValueByCode(string lookUpDomainValuesCode)
        {
            return await _DbContext.LookUpDomainValues.Where(x => x.LookUpDomainCode == lookUpDomainValuesCode).SingleAsync();
        }




    }
}
