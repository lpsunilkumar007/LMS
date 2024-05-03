using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineTestApp.DataAccess.Common;
using OnlineTestApp.Enums.LookUps;
using OnlineTestApp.Domain.LookUps;


namespace OnlineTestApp.DomainLogic.Admin.Common
{
    public static class LookUpDomainValuesDomainLogic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainCode"></param>
        /// <returns></returns>
        public static async Task<List<LookUpDomainValues>> GetLookUpDomainValueByLookUpCode(LookUpDomainCode lookUpDomainCode)
        {
            using (LookUpDomainValuesDataAccess obj = new LookUpDomainValuesDataAccess())
            {
                return await obj.GetLookUpDomainValueByLookUpCode(lookUpDomainCode);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValuesCode"></param>
        /// <returns></returns>
        public static async Task<LookUpDomainValues> GetLookUpDomainValueByCode(LookUpDomainValuesCode lookUpDomainValuesCode)
        {
            return await GetLookUpDomainValueByCode(lookUpDomainValuesCode.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValuesCode"></param>
        /// <returns></returns>
        public static async Task<LookUpDomainValues> GetLookUpDomainValueByCode(string lookUpDomainValuesCode)
        {
            using (LookUpDomainValuesDataAccess obj = new LookUpDomainValuesDataAccess())
            {
                return await obj.GetLookUpDomainValueByCode(lookUpDomainValuesCode.ToString());
            }
        }
    }
}
