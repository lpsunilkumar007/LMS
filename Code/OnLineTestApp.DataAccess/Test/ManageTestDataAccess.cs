using OnlineTestApp.ViewModel.Test;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using OnlineTestApp.Domain.Test;
using System.Threading.Tasks;
using OnlineTestApp.Domain.Question;
using System.Linq.Dynamic;
using OnlineTestApp.Domain.Candidate;

namespace OnlineTestApp.DataAccess.Test
{
    public class ManageTestDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="technology"></param>
        /// <param name="level"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public AddNewTestViewModel PrepareTestBatches(Guid[] technology, Guid[] level, decimal time, int count)
        {
            AddNewTestViewModel result = new AddNewTestViewModel
            {
                LstViewPreparedTestBaches = new List<ViewPreparedTestBaches>()
            };
            var sqlParameter = GetSqlParameters;
            var loggedInUserId = UserVariables.LoggedInUserId;


            for (var i = 0; i < count; i++)
            {
                _SqlParameter.Clear();

                _SqlParameter.Add(new SqlParameter("questionTechnology", string.Join(",", technology)));
                _SqlParameter.Add(new SqlParameter("questionLevel", string.Join(",", technology)));
                _SqlParameter.Add(new SqlParameter("questionTime", time));
                _SqlParameter.Add(new SqlParameter("loggedInUserId", loggedInUserId));
                var questionSet = CallStoreProcToSingleOrDefault<ViewPreparedTestBaches>("GetTestQuestions @questionTechnology, @questionLevel, @questionTime, @loggedInUserId");
                if (questionSet != null)
                {
                    result.LstViewPreparedTestBaches.Add(questionSet);
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDetails"></param>
        /// <returns></returns>
        public async Task AddTestDetails(TestDetails testDetails)
        {
            _DbContext.TestDetails.Add(testDetails);
            await _DbContext.SaveChangesAsync(createLog: false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionIds"></param>
        /// <returns></returns>
        public async Task<List<Questions>> GetQuestionDetails(Guid[] questionIds)
        {
            var result = await _DbContext.Questions
                .Where(x => questionIds.Contains(x.QuestionId))
                .Include(x => x.LstQuestionOptions).ToListAsync();
            foreach (var r in result)
            {
                r.LstQuestionOptions = r.LstQuestionOptions.Where(x => x.IsDeleted == false).ToList();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstQuestionsToAssign"></param>
        /// <param name="testDetailId"></param>
        /// <returns></returns>
        public async Task AssignQuestionToTest(List<Questions> lstQuestionsToAssign, Guid testDetailId)
        {
            int i = 0;
            foreach (var questionsToAssign in lstQuestionsToAssign)
            {
                i++;
                TestQuestions prp = new TestQuestions(questionsToAssign, testDetailId, i);
                _DbContext.TestQuestions.Add(prp);
                //await _DbContext.SaveChangesAsync(createLog: false);
                foreach (var options in questionsToAssign.LstQuestionOptions)
                {
                    prp.LstTestQuestionOptions.Add(new TestQuestionOptions(options, prp.TestQuestionId));
                }
                _DbContext.TestQuestions.Add(prp);
            }
            await _DbContext.SaveChangesAsync(createLog: false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstQuestionTechnology"></param>
        /// <param name="questionId"></param>
        /// <param name="deleteBeforeAssiging"></param>
        /// <returns></returns>
        public async Task AssignTestTechnology(List<TestTechnology> lstQuestionTechnology, Guid testDetailId, bool deleteBeforeAssiging)
        {
            if (deleteBeforeAssiging)
            {
                var existingQuestions = _DbContext.TestTechnology.Where(x => x.FkTestDetailId == testDetailId).ToList();
                if (existingQuestions.Count > 0)
                {
                    foreach (var t in existingQuestions)
                    {
                        _DbContext.Entry(t).State = EntityState.Deleted;
                    }
                }

            }
            //adding new questions to question branches
            foreach (var questionTechnology in lstQuestionTechnology)
            {
                _DbContext.TestTechnology.Add(questionTechnology);
            }
            await _DbContext.SaveChangesAsync(createLog: true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstQuestionLevel"></param>
        /// <param name="questionId"></param>
        /// <param name="deleteBeforeAssiging"></param>
        /// <returns></returns>
        public async Task AssignTestLevel(List<TestLevel> lstQuestionLevel, Guid testDetailId, bool deleteBeforeAssiging)
        {
            if (deleteBeforeAssiging)
            {
                var existingLevels = _DbContext.TestLevel.Where(x => x.FkTestDetailId == testDetailId).ToList();
                if (existingLevels.Count > 0)
                {
                    foreach (var t in existingLevels)
                    {
                        _DbContext.Entry(t).State = EntityState.Deleted;
                    }
                }
            }
            //adding new level to question branches
            foreach (var questionLevel in lstQuestionLevel)
            {
                _DbContext.TestLevel.Add(questionLevel);
            }
            await _DbContext.SaveChangesAsync(createLog: true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDetailId"></param>
        /// <returns></returns>
        public TestDetails GetTestDetailsById(Guid testDetailId)
        {
            return _DbContext.TestDetails
                .Where(x => x.TestDetailId == testDetailId
                      && x.IsDeleted == false).Single();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewTestViewModel"></param>
        /// <returns></returns>
        public async Task<ViewTestViewModel> GetTestDetails(ViewTestViewModel viewTestViewModel)
        {
            var query = _DbContext.TestDetails
                //.Include(x => x.LstTestQuestions)
                //.Include(x => x.LstTestTechnology
                //.Select(y => y.QuestionTechnologies))
                .Where(x => x.IsDeleted == false && x.FkCreatedBy == viewTestViewModel.LoggedInUserId);

            if (!string.IsNullOrEmpty(viewTestViewModel.TestTitle))
            {
                query = query.Where(x => x.TestTitle.Contains(viewTestViewModel.TestTitle));
            }

            if (viewTestViewModel.TestLevel.HasValue)
            {
                query = query.Where(x => x.TestLevel.Any(y => y.FkTestQuestionLevel == viewTestViewModel.TestLevel.Value));
            }

            if (viewTestViewModel.TestTechnology.HasValue)
            {
                query = query.Include(x => x.LstTestTechnology.Select(y => y.QuestionTechnologies));
                query = query.Where(x => x.LstTestTechnology.Any(y => y.FkQuestionTechnology == viewTestViewModel.TestTechnology.Value));
            }

            viewTestViewModel.TotalRecords = await query.CountAsync();
            viewTestViewModel.LstTestDetails = await query.OrderBy(viewTestViewModel.SortBy)
                .Skip(viewTestViewModel.SkipRecords)
                .Take(viewTestViewModel.PageSize).ToListAsync();
            return viewTestViewModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDetails"></param>
        /// <returns></returns>
        public void UpdateTestDetails(TestDetails testDetails)
        {
            var originalData = _DbContext.TestDetails.Where(x => x.TestDetailId == testDetails.TestDetailId && x.IsDeleted == false).Single();

            originalData.TestTitle = testDetails.TestTitle;
            originalData.TestIntroductoryText = testDetails.TestIntroductoryText;
            originalData.ValidUpTo = testDetails.ValidUpTo;
            _DbContext.Entry(originalData).State = EntityState.Modified;
            _DbContext.SaveChanges(createLog: true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDetailId"></param>
        public void DeleteTest(Guid testDetailId)
        {
            Guid loggedInUserId = UserVariables.LoggedInUserId;
            var originalData = _DbContext.TestDetails.Where(x => x.TestDetailId == testDetailId && x.IsDeleted == false).Single();
            originalData.IsDeleted = true;
            originalData.DeletedDateDateTime = DateSettings.CurrentDateTime;
            originalData.FkDeletedBy = loggedInUserId;
            _DbContext.Entry(originalData).State = EntityState.Modified;
            _DbContext.SaveChanges(createLog: false);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tempTestId"></param>
        /// <returns></returns>
        public PreviewTestViewModel GetPreviewTestDetails(Guid tempTestId)
        {
            var tempTestDetails = _DbContext.TempTests.Where(x => x.TempTestId == tempTestId).Single();
            var arrayQuestionIds = tempTestDetails.QuestionIds.Split(',').Select(x => Guid.Parse(x)).ToArray();
            return new PreviewTestViewModel
            {
                TempTests = tempTestDetails,
                LstQuestions = _DbContext.Questions.Where(x => arrayQuestionIds.Contains(x.QuestionId))
                .Include(x => x.LstQuestionOptions).ToList()
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDetailId"></param>
        /// <returns></returns>
        //public async Task<ViewTestDetailsViewModel> GetInvitedTestCandidates(ViewTestDetailsViewModel viewTestDetailsViewModel)
        //{
        //    var query = _DbContext.CandidateAssignedTest
        //        .Include(x => x.CandidateTestDetails).Include(x => x.Candidates).Include(x => x.TestDetails)
        //                 .Where(x => x.TestDetailId == viewTestDetailsViewModel.TestDetailId && x.IsDeleted == false);

        //    viewTestDetailsViewModel.TestDetails = await _DbContext.TestDetails.Where(x => x.TestDetailId == viewTestDetailsViewModel.TestDetailId && x.IsDeleted == false).SingleAsync();
        //    viewTestDetailsViewModel.TotalRecords = await query.CountAsync();
        //    viewTestDetailsViewModel.LstCandidateAssignedTest = await query.OrderBy(viewTestDetailsViewModel.SortBy).Skip(viewTestDetailsViewModel.SkipRecords)
        //        .Take(viewTestDetailsViewModel.PageSize).ToListAsync();
        //    return viewTestDetailsViewModel;

        //    //var query = _DbContext.CandidateTestDetails
        //    //   .Include(x => x.CandidateAssignedTest).Include(x => x.CandidateAssignedTest.Candidates).Include(x => x.CandidateAssignedTest.TestDetails)
        //    //            .Where(x => x.CandidateAssignedTest.TestDetailId == viewTestDetailsViewModel.TestDetailId);

        //    //viewTestDetailsViewModel.TestDetails = await _DbContext.TestDetails.Where(x => x.TestDetailId == viewTestDetailsViewModel.TestDetailId && x.IsDeleted == false).SingleAsync();
        //    //viewTestDetailsViewModel.TotalRecords = await query.CountAsync();
        //    //viewTestDetailsViewModel.LstCandidateAssignedTest = await query.OrderBy(viewTestDetailsViewModel.SortBy).Skip(viewTestDetailsViewModel.SkipRecords)
        //    //    .Take(viewTestDetailsViewModel.PageSize).ToListAsync();
        //    //return viewTestDetailsViewModel;
        //}



        //public CandidateTestDetails GetInvitedTestCandidateTestResults(Guid fkCandidateAssignedTestId)
        //{
        //   return _DbContext.CandidateTestDetails
        //        .Include(x=>x.CandidateAssignedTest)
        //        .Include(x=>x.LstCandidateTestQuestions.Select(y=>y.LstCandidateTestQuestionOptions))
        //        .Where(x => x.FkCandidateAssignedTestId == fkCandidateAssignedTestId && x.IsDeleted == false).Single();
        //}


        //public void DeleteInvitedTestCandidateTest(Guid fkCandidateAssignedTestId)
        //{
        //    Guid loggedInUserId = UserVariables.LoggedInUserId;
        //    var originalData = _DbContext.CandidateAssignedTest.Where(x => x.CandidateAssignedTestId == fkCandidateAssignedTestId && x.IsDeleted == false).Single();
        //    originalData.IsDeleted = true;
        //    originalData.DeletedDateDateTime = DateSettings.CurrentDateTime;
        //    originalData.FkDeletedBy = loggedInUserId;
        //    _DbContext.Entry(originalData).State = EntityState.Modified;
        //    _DbContext.SaveChanges(createLog: false);
        //}

    }
}
