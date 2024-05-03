using OnlineTestApp.DataAccess.Candidate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineTestApp.Admin.DomainLogic.Candidate
{
    public class ManageCandidateDomainLogic : OnlineTestApp.DomainLogic.Admin.BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailIds"></param>
        /// <param name="testDetailId"></param>
        /// <returns></returns>
        public async Task<List<Domain.Candidate.Candidates>> AddMultipleCandidatesViaEmail(string emailAddress, Guid fkCreatedBy, Guid companyId)
        {
            using (ManageCandidateDataAccess obj = new ManageCandidateDataAccess())
            {
                //adding candidate
                return await obj.AddMultipleCandidatesViaEmail(emailAddress.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries), fkCreatedBy, companyId);
            }
        }
    }
}
