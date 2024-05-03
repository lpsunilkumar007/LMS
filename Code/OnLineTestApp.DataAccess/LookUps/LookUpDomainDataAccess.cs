using System;
using System.Linq;

namespace OnlineTestApp.DataAccess.LookUps
{
 public   class LookUpDomainDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainId"></param>
        /// <returns></returns>
        public Domain.LookUps.LookUpDomains GetLookUpDomainById(Guid lookUpDomainId)
        {
            return _DbContext.LookUpDomains.Where(x => x.LookUpDomainId == lookUpDomainId && x.IsDeleted == false).Single();
        }
    }
}
