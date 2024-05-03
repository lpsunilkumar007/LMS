using OnlineTestApp.Domain.Candidate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.DataAccess.Candidate
{
    public class AssignTestToCandidateDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailIds"></param>
        /// <param name="testDetailId"></param>
        /// <returns></returns>
        //public async Task<List<CandidateAssignedTest>> AssignTestToCandidate(Guid[] candidateIds, Guid testDetailId, Guid fkCreatedBy)
        //{
        //    var alreadtAssignedCandidates = await _DbContext.CandidateAssignedTest.Where(x => candidateIds.Contains(x.CandidateId) && x.TestDetailId == testDetailId).ToListAsync();
        //    List<CandidateAssignedTest> result = new List<CandidateAssignedTest>();
        //    foreach (var candidateId in candidateIds)
        //    {
        //        if (!alreadtAssignedCandidates.Where(x => x.CandidateId == candidateId).Any())
        //        {
        //            CandidateAssignedTest candidateAssignedTest = new CandidateAssignedTest();
        //            candidateAssignedTest.CandidateId = candidateId;
        //            candidateAssignedTest.TestDetailId = testDetailId;
        //            candidateAssignedTest.TestReferenceNumber = StringHelper.GenerateTestReferenceNumber();
        //            _DbContext.CandidateAssignedTest.Add(candidateAssignedTest);
        //            result.Add(candidateAssignedTest);
        //        }
        //    }
        //    await _DbContext.SaveChangesAsync(createLog: false);

        //    return result;//alreadtAssignedCandidates.Select(x => x.CandidateId).ToArray();
        //}
    }
}
