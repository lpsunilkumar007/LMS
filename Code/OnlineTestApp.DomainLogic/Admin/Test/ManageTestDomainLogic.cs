using OnlineTestApp.DataAccess.Test;
using OnlineTestApp.ViewModel.Test;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineTestApp.Domain.Test;

using OnlineTestApp.Domain.Candidate;
using OnlineTestApp.DomainLogic.Admin.Candidate;

namespace OnlineTestApp.DomainLogic.Admin.Test
{
    public class ManageTestDomainLogic : BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="technology"></param>
        /// <param name="level"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public AddNewTestViewModel PrepareTestBatches(Guid[] technology, Guid[] level, decimal time)
        {
            using (ManageTestDataAccess obj = new ManageTestDataAccess())
            {
                return obj.PrepareTestBatches(technology: technology, level: level, time: time, count: Utilities.AppSettings.GetIntValue("NumberOfQuestionSets"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addNewTestViewModel"></param>
        //public async Task AddNewTest(AddNewTestViewModel addNewTestViewModel)
        //{
        //    //add test details and get test id
        //    using (ManageTestDataAccess obj = new ManageTestDataAccess())
        //    {
        //        // addNewTestViewModel.TestDetails.TestReferenceNumber = GenerateTestReferenceNumber();
        //        //if (addNewTestViewModel.TestDetails.ValidForDays <= 0)
        //        //{
        //        //    addNewTestViewModel.TestDetails.ValidForDays = ;
        //        //}
        //        addNewTestViewModel.TestDetails.ValidUpTo = addNewTestViewModel.TestDetails.CreatedDateTime.AddDays(SystemSettings.DefaultTestValidForDays);
        //        await obj.AddTestDetails(addNewTestViewModel.TestDetails);

        //        //get question details for test
        //        var questionIds = addNewTestViewModel.LstViewPreparedTestBaches.Where(x => x.IsSelected).Single().QuestionIds.Split(',').Select(x => Guid.Parse(x)).ToArray();
        //        var lstQuestionsToAssign = await obj.GetQuestionDetails(questionIds);

        //        //map all questions with test
        //        await obj.AssignQuestionToTest(lstQuestionsToAssign, addNewTestViewModel.TestDetails.TestDetailId);

        //        //assign technology to test
        //        await AssignTestQuestionTechnology(addNewTestViewModel.QuestionTechnology, addNewTestViewModel.TestDetails.TestDetailId, true);

        //        //assign level to test
        //        await AssignTestLevel(addNewTestViewModel.QuestionLevel, addNewTestViewModel.TestDetails.TestDetailId, true);
        //    }

        //    //get candidate data by from email addres
        //    //check email if exists create it
        //    //assign test to candidate
        //    AssignTestToCandidateDomainLogic objAssignTestToCandidateDomainLogic = new AssignTestToCandidateDomainLogic();
        //    await objAssignTestToCandidateDomainLogic.AssignTestToCandidate(addNewTestViewModel.InviteForTest, addNewTestViewModel.TestDetails);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionLevel"></param>
        /// <param name="testDetailId"></param>
        /// <param name="deleteBeforeAssiging"></param>
        /// <returns></returns>
        private async Task AssignTestLevel(Guid?[] questionLevel, Guid testDetailId, bool deleteBeforeAssiging)
        {
            List<TestLevel> lstTestLevel = new List<TestLevel>();
            foreach (var level in questionLevel)
            {
                lstTestLevel.Add(new TestLevel
                {
                    FkTestDetailId = testDetailId,
                    FkTestQuestionLevel = new Guid(level.ToString()),
                });
            }

            using (ManageTestDataAccess obj = new ManageTestDataAccess())
            {
                await obj.AssignTestLevel(lstTestLevel, testDetailId, deleteBeforeAssiging);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionTechnology"></param>
        /// <param name="testDetailId"></param>
        /// <param name="deleteBeforeAssiging"></param>
        /// <returns></returns>
        private async Task AssignTestQuestionTechnology(Guid?[] questionTechnology, Guid testDetailId, bool deleteBeforeAssiging)
        {
            List<TestTechnology> lstTestTechnology = new List<TestTechnology>();
            foreach (var technology in questionTechnology)
            {
                lstTestTechnology.Add(new TestTechnology
                {
                    FkTestDetailId = testDetailId,
                    FkQuestionTechnology = new Guid(technology.ToString()),
                });
            }

            using (ManageTestDataAccess obj = new ManageTestDataAccess())
            {
                await obj.AssignTestTechnology(lstTestTechnology, testDetailId, deleteBeforeAssiging);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDetailId"></param>
        /// <returns></returns>
        public TestDetails GetTestDetailsById(Guid testDetailId)
        {
            using (ManageTestDataAccess obj = new ManageTestDataAccess())
            {
                return obj.GetTestDetailsById(testDetailId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewTestViewModel"></param>
        /// <returns></returns>
        public async Task<ViewTestViewModel> GetTestDetails(ViewTestViewModel viewTestViewModel)
        {
            switch (viewTestViewModel.SortBy)
            {
                case "TestTitle asc":
                case "TestTitle desc":

                case "TotalCandidates asc":
                case "TotalCandidates desc":

                case "CreatedDateTime asc":
                case "CreatedDateTime desc":

                case "ValidUpTo desc":
                case "ValidUpTo asc":

                    break;

                default:
                    viewTestViewModel.SortBy = "CreatedDateTime desc";
                    break;
            }
            using (ManageTestDataAccess obj = new ManageTestDataAccess())
            {
                viewTestViewModel.QueryString = GetQueryStringsForSorting();
                return await obj.GetTestDetails(viewTestViewModel);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDetails"></param>
        public void UpdateTestDetails(TestDetails testDetails)
        {
            using (ManageTestDataAccess obj = new ManageTestDataAccess())
            {
                obj.UpdateTestDetails(testDetails);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        public void DeleteTest(Guid testDetailId)
        {
            using (ManageTestDataAccess obj = new ManageTestDataAccess())
            {
                obj.DeleteTest(testDetailId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tempTestId"></param>
        /// <returns></returns>
        public PreviewTestViewModel GetPreviewTestDetails(Guid tempTestId)
        {
            using (ManageTestDataAccess obj = new ManageTestDataAccess())
            {
                return obj.GetPreviewTestDetails(tempTestId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDetailId"></param>
        /// <returns></returns>
        //public async Task<ViewTestDetailsViewModel> GetInvitedTestCandidates(ViewTestDetailsViewModel viewTestDetailsViewModel)
        //{
        //    switch (viewTestDetailsViewModel.SortBy)
        //    {
        //        //case "CandidateEmailAddress asc":
        //        //    viewTestDetailsViewModel.SortBy = "CandidateAssignedTest.Candidates.CandidateEmailAddress asc";
        //        //    break;
        //        //case "CandidateEmailAddress desc":
        //        //    viewTestDetailsViewModel.SortBy = "CandidateAssignedTest.Candidates.CandidateEmailAddress desc";
        //        //    break;

        //        default:
        //            viewTestDetailsViewModel.SortBy = "CreatedDateTime desc";
        //            break;
        //    }
        //    using (ManageTestDataAccess obj = new ManageTestDataAccess())
        //    {
        //        viewTestDetailsViewModel.QueryString = GetQueryStringsForSorting();
        //        return await obj.GetInvitedTestCandidates(viewTestDetailsViewModel);
        //    }
        //}

        //public void DeleteInvitedTestCandidateTest(Guid candidateTestDetailId)
        //{
        //    using (ManageTestDataAccess obj = new ManageTestDataAccess())
        //    {
        //        obj.DeleteInvitedTestCandidateTest(candidateTestDetailId);
        //    }
        //}


        //public CandidateTestDetails GetInvitedTestCandidateTestResults(Guid candidateTestDetailId)
        //{
        //    using (ManageTestDataAccess obj = new ManageTestDataAccess())
        //    {
        //        return obj.GetInvitedTestCandidateTestResults(candidateTestDetailId);
        //    }
        //}
    }
}
