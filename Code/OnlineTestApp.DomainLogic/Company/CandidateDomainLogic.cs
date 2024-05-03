using OnlineTestApp.DataAccess.Company;
using OnlineTestApp.Domain.Candidate;
using OnlineTestApp.Domain.TestPaper;
using OnlineTestApp.DomainLogic.Admin.BaseClasses;
using OnlineTestApp.ViewModel.Candidate;
using OnlineTestApp.ViewModel.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Company
{
    public class CandidateDomainLogic : DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDetailsViewModel"></param>
        public void AddCandidateInfo(TestDetailsViewModel testDetailsViewModel)
        {
            using (CandidateDataAccess candidateDataAccess = new CandidateDataAccess())
            {
                candidateDataAccess.AddCandidateInfo(testDetailsViewModel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationId"></param>
        /// <returns></returns>
        public string IsTestValidByTestInvitationId(Guid testInvitationId)
        {
            using (CandidateDataAccess candidateDataAccess = new CandidateDataAccess())
            {
                return candidateDataAccess.IsTestValidByTestInvitationId(testInvitationId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationId"></param>
        /// <returns></returns>
        public CandidateTestResultViewModel GetTestResultById(Guid testInvitationId)
        {
            using (CandidateDataAccess candidateDataAccess = new CandidateDataAccess())
            {
                return candidateDataAccess.GetTestResultById(testInvitationId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationId"></param>
        public void FinishTest(Guid testInvitationId)
        {
            using (CandidateDataAccess candidateDataAccess = new CandidateDataAccess())
            {
                candidateDataAccess.FinishTest(testInvitationId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestQuestionId"></param>
        /// <param name="candidateTestQuestionAnswers"></param>
        /// <returns></returns>
        /// 
        public bool IsLastTestQuestionsByTestInvitationId(Guid testInvitationId)
        {
            using (CandidateDataAccess candidateDataAccess = new CandidateDataAccess())
            {
                return candidateDataAccess.IsLastTestQuestionsByTestInvitationId(testInvitationId);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestDetailsId"></param>
        /// <returns></returns>
        public CandidateTestLeftSideViewModel GetCandidateTestQuestionsInfo(Guid testInvitationId)
        {
            using (CandidateDataAccess candidateDataAccess = new CandidateDataAccess())
            {
                return candidateDataAccess.GetCandidateTestQuestionsInfo(testInvitationId);
            }
        }
        public async Task<bool> SubmitCandidateTestQuestionAnswer(Guid candidateTestQuestionId, string candidateTestQuestionAnswers)
        {

            using (CandidateDataAccess candidateDataAccess = new CandidateDataAccess())
            {
                return await candidateDataAccess.SubmitCandidateTestQuestionAnswer(candidateTestQuestionId, string.IsNullOrEmpty(candidateTestQuestionAnswers) ? new Guid[0] : candidateTestQuestionAnswers.Split(',').Select(x => Guid.Parse(x)).Where(x => x != Guid.Empty).ToArray());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestDetailsId"></param>
        /// <returns></returns>
        public CandidateTestQuestions GetTestQuestionsByTestInvitationId(Guid candidateTestDetailsId)
        {
            using (CandidateDataAccess candidateDataAccess = new CandidateDataAccess())
            {
                return candidateDataAccess.GetTestQuestionsByTestInvitationId(candidateTestDetailsId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPaperId"></param>
        /// <returns></returns>
        public TestPapers GetTestPaperById(Guid testPaperId)
        {
            using (CandidateDataAccess candidateDataAccess = new CandidateDataAccess())
            {
                return candidateDataAccess.GetTestPaperById(testPaperId);

            }
        }
        public TestInvitations VerifyTestTocken(TestTockenVerficationViewModel testTockenVerficationViewModel)
        {
            using (CandidateDataAccess candidateDataAccess = new CandidateDataAccess())
            {
                return candidateDataAccess.VerifyTestTocken(testTockenVerficationViewModel);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationId"></param>
        public void AddCandidateTestQuestionsByTestInvitationId(Guid testInvitationId)
        {
            using (CandidateDataAccess candidateDataAccess = new CandidateDataAccess())
            {
                candidateDataAccess.AddCandidateTestQuestionsByTestInvitationId(testInvitationId);
            }
        }
        public Tuple<string, int> ValidateQuestionByCandidateTestQuestionId(Guid candidateTestQuestionId)
        {
            using (CandidateDataAccess candidateDataAccess = new CandidateDataAccess())
            {
                return candidateDataAccess.ValidateQuestionByCandidateTestQuestionId(candidateTestQuestionId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationId"></param>
        /// <returns></returns>
        public Boolean IsTestAttemptedByTestInvitationId(Guid testInvitationId)
        {
            using (CandidateDataAccess CandidateDataAccess = new CandidateDataAccess())
            {
                return CandidateDataAccess.IsTestAttemptedByTestInvitationId(testInvitationId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateQuestionId"></param>
        /// <returns></returns>
        public async Task<bool> CandidateQuestionSkipped(Guid candidateQuestionId)
        {
            using (CandidateDataAccess CandidateDataAccess = new CandidateDataAccess())
            {
                return await CandidateDataAccess.CandidateQuestionSkipped(candidateQuestionId);
            }
        }

    }
}
