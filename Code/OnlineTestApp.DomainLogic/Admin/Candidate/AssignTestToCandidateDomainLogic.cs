using OnlineTestApp.Admin.DomainLogic.Candidate;
using OnlineTestApp.DataAccess.Candidate;
using OnlineTestApp.Domain.Candidate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.Candidate
{
    public class AssignTestToCandidateDomainLogic : BaseClasses.DomainLogicBase
    {         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateIds"></param>
        /// <param name="testDetailId"></param>
        /// <param name="fkCreatedBy"></param>
        /// <returns></returns>
        //public async Task AssignTestToCandidate(Domain.Email.SendEmail inviteForTest, Domain.Test.TestDetails testDetails)
        //{
        //    ManageCandidateDomainLogic objManageCandidateDomainLogic = new ManageCandidateDomainLogic();
        //    var fkCreatedBy = UserVariables.LoggedInUserId;
        //    var candidates = await objManageCandidateDomainLogic.AddMultipleCandidatesViaEmail(inviteForTest.EmailToEmailAddress, fkCreatedBy, UserVariables.UserCompanyId);

        //    List<CandidateAssignedTest> lstAssignedTestCandidates = null;
        //    using (AssignTestToCandidateDataAccess obj = new AssignTestToCandidateDataAccess())
        //    {
        //        lstAssignedTestCandidates = await obj.AssignTestToCandidate(
        //            candidateIds: candidates.Select(x => x.CandidateId).ToArray()
        //            , testDetailId: testDetails.TestDetailId
        //            , fkCreatedBy: fkCreatedBy);
        //    }            
            
        //    if (lstAssignedTestCandidates.Count > 0)
        //    {                
        //        EmailCandidateDomainLogic emailCandidateDomainLogic = new EmailCandidateDomainLogic();
        //        await emailCandidateDomainLogic.InviteCandidateForTest(new ViewModel.Email.EmailCandidateViewModel
        //        {
        //            LstCandidateAssignedTest = lstAssignedTestCandidates,
        //            SendEmail = inviteForTest
        //        }, fkCreatedBy);
        //    }
        //}
    }
}
