using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.DataAccess.Email
{
    public class SendEmailDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public async Task<Domain.Candidate.Candidates> GetCandidateData(Guid candidateId)
        {
            return await _DbContext.Candidates
                                       
                                        
                                         .Where(x =>  x.CandidateId == candidateId)
                                         .SingleAsync();
        }
    }
}
