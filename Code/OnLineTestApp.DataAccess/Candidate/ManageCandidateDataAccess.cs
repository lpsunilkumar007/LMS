using OnlineTestApp.Domain.Candidate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.DataAccess.Candidate
{
    public class ManageCandidateDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailIds"></param>
        /// <param name="fkCreatedBy"></param>
        /// <returns></returns>
        public async Task<List<Candidates>> AddMultipleCandidatesViaEmail(string[] arrayEmailIds, Guid fkCreatedBy, Guid companyId)
        {
            //var arrayEmailIds = emailIds.Split(',').ToArray();
            var lstCandidates = await _DbContext.Candidates.Where(x => arrayEmailIds.Contains(x.CandidateEmailAddress)).ToListAsync();

            foreach (var candidateEmail in arrayEmailIds)
            {
                if (!lstCandidates.Where(x => x.CandidateEmailAddress == candidateEmail).Any())
                {
                    var candidate = new Candidates()
                    {
                        CandidateEmailAddress = candidateEmail,
                        CandidateName = "",
                        FkCreatedBy = fkCreatedBy,
                        FKCompanyId = companyId
                    };
                    _DbContext.Candidates.Add(candidate);
                }
            }
            await _DbContext.SaveChangesAsync(createLog: false);

            return _DbContext.Candidates.Where(x => arrayEmailIds.Contains(x.CandidateEmailAddress)).ToList();
        }

    }
}
