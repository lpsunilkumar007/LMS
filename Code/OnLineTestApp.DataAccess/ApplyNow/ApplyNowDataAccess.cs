using OnlineTestApp.Domain.Candidate;
using OnlineTestApp.Domain.Test;
using OnlineTestApp.ViewModel.ApplyNow;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.DataAccess.ApplyNow
{
    public class ApplyNowDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Step1CheckReferenceViewModel"></param>
        /// <returns></returns>
        public string ValidateTestByReferenceNumber(string referenceNumber)
        {
            var referenceExits = _DbContext.CandidateAssignedTest.Where(x => x.TestReferenceNumber == referenceNumber).SingleOrDefault();
            if (referenceExits == null)
            {
                return "Refernce number does not exists";
            }

            var queryExists = _DbContext.CandidateTestDetails.Where(x => x.FkCandidateAssignedTestId == referenceExits.CandidateAssignedTestId).SingleOrDefault();

            if (queryExists == null)//is test assigned
            {
                return ""; // start test
            }
            if (queryExists != null && queryExists.IsTestExpired)
            {
                return "Test time expired";
            }
            if (queryExists.IsCompleted == true)
            {
                return "Test already completed";
            }

            return string.Empty;
        }
        /// <summary>
        / 
        / </summary>
        / <param name = "referenceNumber" ></ param >
        / < returns ></ returns >
        public Step2ConfirmationViewModel GetTestDetailsByReferenceNumber(string referenceNumber)
        {
            var CandidateAssignedTest = _DbContext.CandidateAssignedTest.Include(x => x.TestDetails).Where(x => x.TestReferenceNumber == referenceNumber).Single();
            return new Step2ConfirmationViewModel
            {
                CandidateAssignedId = CandidateAssignedTest.CandidateAssignedTestId,
                TestDetails = CandidateAssignedTest.TestDetails,
                ReferenceNumber = referenceNumber
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestDetailsId"></param>
        /// <returns></returns>
        /// 

        public Tuple<string, int> ValidateQuestionByCandidateTestQuestionId(Guid candidateTestQuestionId)
        {
            //var result = new Tuple<string, int>("",0);
            var questionDetailsExits = _DbContext.CandidateTestQuestions.Where(x => x.CandidateTestQuestionId == candidateTestQuestionId).SingleOrDefault();


            if (questionDetailsExits.IsQuestionAnswered == true)
            {
                return new Tuple<string, int>("This question is already answered, you can't reattempt it", -1);
            }
            TimeSpan questionDuration = DateSettings.CurrentDateTime - questionDetailsExits.StartTime.Value;

            if (questionDuration.TotalMinutes > questionDetailsExits.TotalTime)
            {
                return new Tuple<string, int>("Question time expired", -2);
            }
            return null;
        }
        public string ValidateTestByCandidateTestDetailsId(Guid fkCandidateAssignedTestId)
        {
            var testDetailsExits = _DbContext.CandidateTestDetails.Where(x => x.FkCandidateAssignedTestId == fkCandidateAssignedTestId).SingleOrDefault();
            if (testDetailsExits == null)
            {
                return "Candidate test does not exists";
            }
            if (testDetailsExits.EndTime < DateSettings.CurrentDateTime)
            {
                return "Test time expired";
            }
            if (testDetailsExits.IsCompleted)
            {
                return "Test already completed";
            }
            return string.Empty;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestDetailsId"></param>
        /// <returns></returns>
        public async Task<TestDetails> GetTestDetailsByCandidateTestDetailsId(Guid fkCandidateAssignedTestId)
        {
            return await _DbContext.TestDetails
            .Join(_DbContext.CandidateAssignedTest, x => x.TestDetailId, y => y.TestDetailId, (x, y) => new { TestDetails = x, CandidateAssignedTest = y })
            .Join(_DbContext.CandidateTestDetails, p => p.CandidateAssignedTest.CandidateAssignedTestId, q => q.FkCandidateAssignedTestId, (p, q) => new
            {
                CandidateAssignedTest = p,
                CandidateTestDetails = q
            })
             .Where(q => q.CandidateTestDetails.FkCandidateAssignedTestId == fkCandidateAssignedTestId)
             .Select(y => y.CandidateAssignedTest.TestDetails).FirstOrDefaultAsync();
        }
        / <summary>
        / 
        / </summary>
        / <param name = "candidateTestDetailId" ></ param >
        / < returns ></ returns >
        public CandidateTestQuestions GetTestQuestionsByCandidateTestDetailsId(Guid candidateTestDetailId)
        {
            var result = _DbContext.CandidateTestQuestions.Include(x => x.LstCandidateTestQuestionOptions)
                 .Where(x => x.FkCandidateAssignedTestId == candidateTestDetailId && x.IsQuestionAnswered == false

                 &&

                 x.IsDeleted == false &&
               (!(x.EndTime.HasValue) || (x.EndTime > DateSettings.CurrentDateTime))
                  && x.IsSkippedAnswered == false)
                 .OrderBy(x => x.DisplayOrder).ThenBy(y => y.CreatedDateTime)
                 .FirstOrDefault();

            if (result != null)
            {
                result.LstCandidateTestQuestionOptions = result.LstCandidateTestQuestionOptions.Where(y => y.IsDeleted == false).OrderBy(y => y.DisplayOrder).ToList();
                //start time and end time save
                UpdateTestQuestionsTimeByCandidateTestQuestionId(result.CandidateTestQuestionId);
            }
            return result;
        }

        public void UpdateTestQuestionsTimeByCandidateTestQuestionId(Guid candidateTestQuestionId)
        {
            var originalDataTestQuestion = _DbContext.CandidateTestQuestions.Where(x => x.CandidateTestQuestionId == candidateTestQuestionId && x.IsDeleted == false).Single();
            if (!originalDataTestQuestion.StartTime.HasValue)
            {
                //Update test start time and end time
                originalDataTestQuestion.StartTime = DateSettings.CurrentDateTime;
                originalDataTestQuestion.EndTime = DateSettings.CurrentDateTime.AddMinutes(originalDataTestQuestion.TotalTime);
                _DbContext.Entry(originalDataTestQuestion).State = EntityState.Modified;
                _DbContext.SaveChanges(createLog: false);
            }
        }

        / <summary>
        / 
        / </summary>
        / <param name = "candidateTestQuestionId" ></ param >
        / < param name="candidateTestQuestionAnswers"></param>
        / <returns></returns>
        public async Task<bool> SubmitCandidateTestQuestionAnswer(Guid candidateTestQuestionId, Guid[] candidateTestQuestionAnswers)
        {
            //Update test question option IsCandidateAnswered

            foreach (var data in candidateTestQuestionAnswers)
            {
                var originalData = _DbContext.CandidateTestQuestionOptions.Where(x => x.CandidateQuestionOptionId == data && x.IsDeleted == false).Single();
                originalData.IsCandidateAnswered = true;
                _DbContext.Entry(originalData).State = EntityState.Modified;
            }
            var originalDataTestQuestion = _DbContext.CandidateTestQuestions.Where(x => x.CandidateTestQuestionId == candidateTestQuestionId && x.IsDeleted == false).Single();

            //Update test question IsQuestionAnswered 
            //originalDataTestQuestion.IsQuestionAnswered = true;
            //_DbContext.Entry(originalDataTestQuestion).State = EntityState.Modified;

            await _DbContext.SaveChangesAsync(createLog: false);

            //var IsTestLastQuestion = _DbContext.CandidateTestQuestions.Where(x => x.FkCandidateAssignedTestId == originalDataTestQuestion.FkCandidateAssignedTestId
            // && x.IsDeleted == false && x.IsQuestionAnswered == false && x.IsSkippedAnswered == false).ToList().Count();
            //return IsTestLastQuestion > 0 ? true : false;
        }

        public async Task<bool> CandidateQuestionSkipped(Guid candidateTestQuestionId)
        {
            var originalDataTestQuestion = _DbContext.CandidateTestQuestions.Where(x => x.CandidateTestQuestionId == candidateTestQuestionId && x.IsDeleted == false).Single();

            //Update test question IsSkippedAnswered 
            originalDataTestQuestion.IsSkippedAnswered = true;
            _DbContext.Entry(originalDataTestQuestion).State = EntityState.Modified;

            await _DbContext.SaveChangesAsync(createLog: false);

            var IsTestLastQuestion = _DbContext.CandidateTestQuestions.Where(
             //x => x.FkCandidateAssignedTestId == originalDataTestQuestion.FkCandidateAssignedTestId
             // && 
             x => x.IsDeleted == false && x.IsQuestionAnswered == false && x.IsSkippedAnswered == false).ToList().Count();
            return IsTestLastQuestion > 0 ? true : false;
        }

        /**/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDetailId"></param>
        /// <returns></returns>
        public TestDetails GetTestDetailsToAssignToCandidate(Guid testDetailId)
        {
            return _DbContext.TestDetails.Include(x => x.LstTestQuestions.Select(y => y.LstTestQuestionOptions)).
                Where(x => x.TestDetailId == testDetailId).Single();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateAssignedId"></param>
        /// <returns></returns>
        public Guid? GetCandidateTestDetailsIdByCandidateAssignedId(Guid fkCandidateAssignedTestId)
        {
            return _DbContext.CandidateTestDetails.Where(x => x.FkCandidateAssignedTestId == fkCandidateAssignedTestId).Select(x => x.FkCandidateAssignedTestId).SingleOrDefault();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestDetails"></param>
        /// <returns></returns>
        public async Task AddCandidateTestDetails(CandidateTestDetails candidateTestDetails)
        {
            _DbContext.CandidateTestDetails.Add(candidateTestDetails);
            await _DbContext.SaveChangesAsync(createLog: false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstTestQuestionsToAssign"></param>
        /// <param name="testDetailId"></param>
        /// <returns></returns>
        public async Task AssignQuestionToCandidateTest(List<TestQuestions> lstTestQuestionsToAssign, Guid candidateTestDetailId)
        {
            int i = 0;
            foreach (var testQuestionsToAssign in lstTestQuestionsToAssign)
            {
                i++;
                CandidateTestQuestions prp = new CandidateTestQuestions(testQuestionsToAssign, candidateTestDetailId, i);
                _DbContext.CandidateTestQuestions.Add(prp);
                foreach (var options in testQuestionsToAssign.LstTestQuestionOptions)
                {
                    if (options.IsDeleted == true) continue;
                    prp.LstCandidateTestQuestionOptions.Add(new CandidateTestQuestionOptions(options, prp.CandidateTestQuestionId));
                }
                _DbContext.CandidateTestQuestions.Add(prp);
            }
            await _DbContext.SaveChangesAsync(createLog: false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestId"></param>
        /// <returns></returns>
        public async Task<CandidateTestDetails> GetCandidateTestDetailsById(Guid fkCandidateAssignedTestId)
        {
            return await _DbContext.CandidateTestDetails.Where(x => x.FkCandidateAssignedTestId == fkCandidateAssignedTestId).SingleOrDefaultAsync();
        }
    }
}
