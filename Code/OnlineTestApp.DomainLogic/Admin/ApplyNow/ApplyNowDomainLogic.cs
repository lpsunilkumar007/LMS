using OnlineTestApp.DataAccess.ApplyNow;
using OnlineTestApp.Domain.Candidate;
using OnlineTestApp.ViewModel.ApplyNow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.ApplyNow
{
    public class ApplyNowDomainLogic : BaseClasses.DomainLogicBase
    {
        //#region Step 1
        ///// <summary>
        ///// STep 1
        ///// </summary>
        ///// <param name="step1CheckReferenceViewModel"></param>
        ///// <returns></returns>
        //public string ValidateTestByReferenceNumber(string referenceNumber)
        //{
        //    using (ApplyNowDataAccess obj = new ApplyNowDataAccess())
        //    {
        //        return obj.ValidateTestByReferenceNumber(referenceNumber);
        //    }
        //}
        //#endregion

        //#region Step 2
        ///// <summary>
        ///// Step 2
        ///// </summary>
        ///// <param name="referenceNumber"></param>
        ///// <returns></returns>
        //public Step2ConfirmationViewModel GetTestDetailsByReferenceNumber(string referenceNumber)
        //{
        //    using (ApplyNowDataAccess obj = new ApplyNowDataAccess())
        //    {
        //        return obj.GetTestDetailsByReferenceNumber(referenceNumber);
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDetailId"></param>
        /// <returns></returns>
        public async Task<Guid> AssignTestToCandidate(Guid testDetailId, Guid candidateAssignedId)
        {
            using (ApplyNowDataAccess obj = new ApplyNowDataAccess())
            {
                var testDetails = obj.GetTestDetailsToAssignToCandidate(testDetailId);
                CandidateTestDetails candidateTestDetails = new CandidateTestDetails();
                Guid? candidateTestDetailsId = obj.GetCandidateTestDetailsIdByCandidateAssignedId(candidateAssignedId);
                if (candidateTestDetailsId == Guid.Empty)
                {

                    //Fetch on run time
                    candidateTestDetails.EndTime = DateSettings.CurrentDateTime.AddMinutes(testDetails.TotalTime);
                    candidateTestDetails.TotalQuestions = testDetails.LstTestQuestions.Count();
                    candidateTestDetails.FkCandidateAssignedTestId = candidateAssignedId;
                    //adding test details
                    await obj.AddCandidateTestDetails(candidateTestDetails);

                    //adding test questions
                    await obj.AssignQuestionToCandidateTest(testDetails.LstTestQuestions, candidateTestDetails.FkCandidateAssignedTestId);

                    return candidateTestDetails.FkCandidateAssignedTestId;
                }
                return candidateTestDetailsId.Value;
            }
        }
        //#endregion


        //public Tuple<string, int> ValidateQuestionByCandidateTestQuestionId(Guid candidateTestQuestionId)
        //{
        //    using (ApplyNowDataAccess obj = new ApplyNowDataAccess())
        //    {
        //        return obj.ValidateQuestionByCandidateTestQuestionId(candidateTestQuestionId);
        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="candidateTestDetailsId"></param>
        ///// <returns></returns>
        //public string ValidateTestByCandidateTestDetailsId(Guid candidateTestDetailsId)
        //{
        //    using (ApplyNowDataAccess obj = new ApplyNowDataAccess())
        //    {
        //        return obj.ValidateTestByCandidateTestDetailsId(candidateTestDetailsId);
        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="candidateTestDetailsId"></param>
        ///// <returns></returns>
        //public async Task<Step3CandidateTestViewModel> GetTestDetailsByCandidateTestDetailsId(Guid candidateTestDetailsId)
        //{
        //    Step3CandidateTestViewModel step3CandidateTestViewModel = new Step3CandidateTestViewModel();
        //    using (ApplyNowDataAccess obj = new ApplyNowDataAccess())
        //    {
        //        step3CandidateTestViewModel.TestDetails = await obj.GetTestDetailsByCandidateTestDetailsId(candidateTestDetailsId);
        //        step3CandidateTestViewModel.CandidateTestDetails = await obj.GetCandidateTestDetailsById(candidateTestDetailsId);
        //    }
        //    return step3CandidateTestViewModel;
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestDetailsId"></param>
        /// <returns></returns>
        //public CandidateTestQuestions GetTestQuestionsByCandidateTestDetailsId(Guid candidateTestDetailsId)
        //{
        //    //using (ApplyNowDataAccess obj = new ApplyNowDataAccess())
        //    //{
        //    //    return obj.GetTestQuestionsByCandidateTestDetailsId(candidateTestDetailsId);
        //    //}
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestQuestionId"></param>
        /// <param name="candidateTestQuestionAnswers"></param>
        /// <returns></returns>
        //public async Task<bool> SubmitCandidateTestQuestionAnswer(Guid candidateTestQuestionId, string candidateTestQuestionAnswers)
        //{
        //    using (ApplyNowDataAccess obj = new ApplyNowDataAccess())
        //    {
        //        return await obj.SubmitCandidateTestQuestionAnswer(candidateTestQuestionId, string.IsNullOrEmpty(candidateTestQuestionAnswers) ? new Guid[0] : candidateTestQuestionAnswers.Split(',').Select(x => Guid.Parse(x)).Where(x => x != Guid.Empty).ToArray());
        //    }
        //}


        //public async Task<bool> CandidateQuestionSkipped(Guid candidateQuestionId)
        //{
        //    using (ApplyNowDataAccess obj = new ApplyNowDataAccess())
        //    {
        //        return await obj.CandidateQuestionSkipped(candidateQuestionId);
        //    }
        //}


        /**/




    }
}
