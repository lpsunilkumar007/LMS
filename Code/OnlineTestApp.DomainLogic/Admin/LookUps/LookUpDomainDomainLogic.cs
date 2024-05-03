using OnlineTestApp.DataAccess.LookUps;
using System;

namespace OnlineTestApp.DomainLogic.Admin.LookUps
{
    public class LookUpDomainDomainLogic : OnlineTestApp.DomainLogic.Admin.BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainId"></param>
        /// <returns></returns>
        public Domain.LookUps.LookUpDomains GetLookUpDomainById(Guid lookUpDomainId)
        {
            using (LookUpDomainDataAccess obj = new LookUpDomainDataAccess())
            {
                return obj.GetLookUpDomainById(lookUpDomainId);
            }
        }
    }
}
