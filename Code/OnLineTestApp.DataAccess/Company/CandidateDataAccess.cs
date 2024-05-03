using OnlineTestApp.Domain.TestPaper;
using OnlineTestApp.ViewModel.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using OnlineTestApp.Domain.Candidate;
using OnlineTestApp.Domain.Test;
using OnlineTestApp.Domain.Question;
using OnlineTestApp.ViewModel.Candidate;

namespace OnlineTestApp.DataAccess.Company
{
    public class CandidateDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// to add candidate info in db table 
        /// </summary>
        /// <param name="testDetailsViewModel"></param>
        public void AddCandidateInfo(TestDetailsViewModel testDetailsViewModel)
        {
            var originalData = _DbContext.TestInvitations.Where(x => x.TestInvitationId == testDetailsViewModel.FkTestInvitationId && x.IsDeleted == false).Single();
            originalData.CandidateName = testDetailsViewModel.CandidateName;
            _DbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationId"></param>
        /// <returns></returns>
        public string IsTestValidByTestInvitationId(Guid testInvitationId)
        {
            var testDetails = _DbContext.TestInvitations.Where(x => x.TestInvitationId == testInvitationId).FirstOrDefault();
            if (testDetails == null)
            {
                return "No test found!";
            }
            else if (testDetails.IsTestFinished)
            {
                return "Test  is already given please contact admin for new test!";
            }
            return null;
        }
        public CandidateTestResultViewModel GetTestResultById(Guid testInvitationId)
        {

            CandidateTestResultViewModel candidateTestResultViewModel = new CandidateTestResultViewModel();
            candidateTestResultViewModel.TestInvitation = _DbContext.TestInvitations.Where(x => x.TestInvitationId == testInvitationId).
                Include(x => x.TestPaper).

                FirstOrDefault();
            candidateTestResultViewModel.LstCandidateTestQuestions = _DbContext.CandidateTestQuestions.
                Include(x => x.QuestionTechnology).
                Where(x => x.FkTestInvitationId == testInvitationId).OrderBy(x => x.DisplayOrder).ToList();
            return candidateTestResultViewModel;
        }
        /// <summary>
        /// after end test all calculation for total, obtained and -ve  marking 
        /// </summary>
        /// <param name="testInvitationId"></param>
        public void FinishTest(Guid testInvitationId)
        {
            var originalData = _DbContext.TestInvitations.Where(x => x.TestInvitationId == testInvitationId && x.IsDeleted == false).Single();
            originalData.IsTestFinished = true;
            // calculate here all perameter for 
            List<CandidateTestQuestions> listQuestionAnswerd = _DbContext.CandidateTestQuestions.Where(x => x.FkTestInvitationId == testInvitationId).ToList();
            if (listQuestionAnswerd != null)
            {
                originalData.TotalCorrectAnswers = listQuestionAnswerd.Where(x => x.IsFullyCorrectAnswered == true).ToList().Count;
                originalData.TotalAnsweredQuestions = listQuestionAnswerd.Where(x => x.IsQuestionAnswered == true).ToList().Count;
                decimal CorrectQuesionsScore = listQuestionAnswerd.Where(x => x.IsFullyCorrectAnswered == true).ToList().Sum(x => x.TotalScore);
                decimal negativeMarks = listQuestionAnswerd.Where(x => x.IsFullyCorrectAnswered == false && x.IsQuestionAnswered == true && x.IsNegativeMarking == true).ToList().Sum(x => x.TotalScore);
                originalData.TotalCandidateScoreObtained = CorrectQuesionsScore - negativeMarks;
                //pass fail 
                if (((originalData.TotalCandidateScoreObtained / originalData.TotalScore) * 100) >= originalData.PassingPercentage)
                {
                    originalData.IsPassed = true;
                }
            }
            _DbContext.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationId"></param>
        /// <returns></returns>
        public CandidateTestLeftSideViewModel GetCandidateTestQuestionsInfo(Guid testInvitationId)
        {
            var testquestions = _DbContext.CandidateTestQuestions.Where(x => x.FkTestInvitationId == testInvitationId).ToList();
            if (testquestions == null) return null;
            CandidateTestLeftSideViewModel candidateTestLeftSideViewModel = new CandidateTestLeftSideViewModel
            {
                TotalQuestions = testquestions.Where(x => x.IsDeleted == false).Count(),
                AttemptedQuestions = testquestions.Where(x => x.IsQuestionAnswered == true).Count(),
                SkippedQuestions = testquestions.Where(x => x.IsSkippedAnswered == true).Count(),
            };
            return candidateTestLeftSideViewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationId"></param>
        /// <returns></returns>
        public bool IsLastTestQuestionsByTestInvitationId(Guid testInvitationId)
        {
            var lastQuestion = _DbContext.CandidateTestQuestions.Include(x => x.LstCandidateTestQuestionOptions)
               .Where(x => x.FkTestInvitationId == testInvitationId && x.IsQuestionAnswered == false && x.IsDeleted == false

               //&&(!(x.EndTime.HasValue) || (x.EndTime > DateSettings.CurrentDateTime))

               && x.IsSkippedAnswered == false)
               .OrderByDescending(x => x.DisplayOrder).ThenBy(y => y.CreatedDateTime)
               .FirstOrDefault();

            var result = _DbContext.CandidateTestQuestions.Include(x => x.LstCandidateTestQuestionOptions)
                .Where(x => x.FkTestInvitationId == testInvitationId && x.IsQuestionAnswered == false && x.IsDeleted == false
                 &&
                (!(x.EndTime.HasValue) || (x.EndTime > DateSettings.CurrentDateTime)
                 )
                 && x.IsSkippedAnswered == false)
                .OrderBy(x => x.DisplayOrder).ThenBy(y => y.CreatedDateTime)
                .FirstOrDefault();

            if (result.CandidateTestQuestionId == lastQuestion.CandidateTestQuestionId)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestQuestionId"></param>
        /// <returns></returns>
        public async Task<bool> CandidateQuestionSkipped(Guid candidateTestQuestionId)
        {
            var originalDataTestQuestion = _DbContext.CandidateTestQuestions.Where(x => x.CandidateTestQuestionId == candidateTestQuestionId && x.IsDeleted == false).Single();

            //Update test question IsSkippedAnswered 
            originalDataTestQuestion.IsSkippedAnswered = true;
            _DbContext.Entry(originalDataTestQuestion).State = EntityState.Modified;

            await _DbContext.SaveChangesAsync(createLog: false);

            var IsTestLastQuestion = _DbContext.CandidateTestQuestions.Where(x => x.FkTestInvitationId == originalDataTestQuestion.FkTestInvitationId
             && x.IsDeleted == false && x.IsQuestionAnswered == false && x.IsSkippedAnswered == false).ToList().Count();
            return IsTestLastQuestion > 0 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestQuestionId"></param>
        /// <param name="candidateTestQuestionAnswers"></param>
        /// <returns></returns>
        public async Task<bool> SubmitCandidateTestQuestionAnswer(Guid candidateTestQuestionId, Guid[] candidateTestQuestionAnswers)
        {
            //Update test question option IsCandidateAnswered
            bool isCandidaidateAnswerFullyCorrect = true;// is answered Question is correct or not 
            foreach (var data in candidateTestQuestionAnswers)
            {
                var originalData = _DbContext.CandidateTestQuestionOptions.Where(x => x.CandidateQuestionOptionId == data && x.IsDeleted == false).Single();
                originalData.IsCandidateAnswered = true;
                if (!originalData.IsCorrect)
                {
                    isCandidaidateAnswerFullyCorrect = false;
                }
                _DbContext.Entry(originalData).State = EntityState.Modified;
            }
            var originalDataTestQuestion = _DbContext.CandidateTestQuestions.Where(x => x.CandidateTestQuestionId == candidateTestQuestionId && x.IsDeleted == false).Single();

            //Update test question IsQuestionAnswered 
            originalDataTestQuestion.IsQuestionAnswered = true;
            originalDataTestQuestion.IsFullyCorrectAnswered = isCandidaidateAnswerFullyCorrect;
            // _DbContext.Entry(originalDataTestQuestion).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync(createLog: false);
            var IsTestLastQuestion = _DbContext.CandidateTestQuestions.Where(x => x.FkTestInvitationId == originalDataTestQuestion.FkTestInvitationId
             && x.IsDeleted == false && x.IsQuestionAnswered == false && x.IsSkippedAnswered == false).ToList().Count();
            return IsTestLastQuestion > 0 ? true : false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestDetailId"></param>
        /// <returns></returns>
        public CandidateTestQuestions GetTestQuestionsByTestInvitationId(Guid testInvitationId)
        {
            var origionalQuestion = _DbContext.CandidateTestQuestions.Include(x => x.LstCandidateTestQuestionOptions)
                 .Where(x => x.FkTestInvitationId == testInvitationId && x.IsQuestionAnswered == false && x.IsDeleted == false
                  &&
                    (!(x.EndTime.HasValue) || (x.EndTime > DateSettings.CurrentDateTime)
                  )
                  && x.IsSkippedAnswered == false)
                 .OrderBy(x => x.DisplayOrder).ThenBy(y => y.CreatedDateTime)
                 .FirstOrDefault();

            if (origionalQuestion != null)
            {
                origionalQuestion.LstCandidateTestQuestionOptions = origionalQuestion.LstCandidateTestQuestionOptions.Where(y => y.IsDeleted == false).OrderBy(y => y.DisplayOrder).ToList();
                //start time and end time save
                if (!origionalQuestion.StartTime.HasValue)
                {
                    UpdateTestQuestionsTimeByCandidateTestQuestionId(origionalQuestion.CandidateTestQuestionId);
                }
            }
            return origionalQuestion;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CandidateTestQuestionId"></param>
        public void UpdateTestQuestionsTimeByCandidateTestQuestionId(Guid CandidateTestQuestionId)
        {
            var originalDataTestQuestion = _DbContext.CandidateTestQuestions.Where(x => x.CandidateTestQuestionId == CandidateTestQuestionId && x.IsDeleted == false).Single();
            originalDataTestQuestion.StartTime = DateSettings.CurrentDateTime;
            originalDataTestQuestion.EndTime = DateSettings.CurrentDateTime.AddMinutes(originalDataTestQuestion.TotalTime);

            _DbContext.SaveChanges(createLog: false);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationId"></param>
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
        public Boolean IsTestAttemptedByTestInvitationId(Guid testInvitationId)
        {
            TestInvitations testInvitation = _DbContext.TestInvitations.Where(x => x.TestInvitationId == testInvitationId && x.IsDeleted == false && x.IsAttempted == false).FirstOrDefault();
            //check if 
            // testInvitation.IsAttempted = true;

            if (testInvitation != null) { return false; } else { return true; };

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationId"></param>
        public void AddCandidateTestQuestionsByTestInvitationId(Guid testInvitationId)
        {
            TestInvitations testInvitation = _DbContext.TestInvitations.Where(x => x.TestInvitationId == testInvitationId && x.IsDeleted == false && x.IsAttempted == false).FirstOrDefault();
            // fetch all the question of test paper 
            var testPaper = _DbContext.TestPapers.Where(x => x.TestPaperId == testInvitation.FkTestPaperId && x.IsDeleted == false).FirstOrDefault();
            testInvitation.IsAttempted = true;
            //testInvitation.PassingPercentage = testInvitation.PassingPercentage;
            _DbContext.Entry(testInvitation).State = EntityState.Modified;

            List<TestPaperQuestions> listTestpaperQuestions = _DbContext.TestPaperQuestions.
                Include(x => x.Question).
                Include(y => y.Question.LstQuestionOptions)
               .Where(x => x.IsDeleted == false && x.FkTestPaperId == testInvitation.FkTestPaperId).ToList();

            if (listTestpaperQuestions.Count > 0)
            {
                int i = 0;
                foreach (var testQuestionsToAssign in listTestpaperQuestions)
                {
                    i++;
                    CandidateTestQuestions questions = new CandidateTestQuestions
                    {
                        CandidateTestQuestionId = Guid.NewGuid(),
                        FkTestInvitationId = testInvitation.TestInvitationId,
                        FkTestPaperId = testInvitation.FkTestPaperId,
                        FieldType = testQuestionsToAssign.Question.FieldType,
                        TotalScore = testQuestionsToAssign.Question.TotalScore,
                        DisplayOrder = i,
                        QuestionTitle = testQuestionsToAssign.Question.QuestionTitle,
                        QuestionDescription = testQuestionsToAssign.Question.QuestionDescription,
                        TotalTime = testQuestionsToAssign.Question.TotalTime,
                        CanSkipQuestion = testQuestionsToAssign.Question.CanSkipQuestion,
                        MaxScore = testQuestionsToAssign.Question.MaxScore,
                        NegativeMarks = testQuestionsToAssign.Question.NegativeMarks,
                        LstCandidateTestQuestionOptions = new List<CandidateTestQuestionOptions>(),
                        TotalOptions = testQuestionsToAssign.Question.TotalOptions,
                        IsNegativeMarking = testPaper.IsNegativeMarking,
                        FkQuestionTechnologyId = testQuestionsToAssign.FkQuestionTechnologyId.Value

                    };
                    _DbContext.CandidateTestQuestions.Add(questions);
                    // insert options 
                    foreach (var options in testQuestionsToAssign.Question.LstQuestionOptions)
                    {
                        if (options.IsDeleted == true) continue;
                        CandidateTestQuestionOptions candidateTestQuestionOptions = new CandidateTestQuestionOptions
                        {
                            FkQuestionId = questions.CandidateTestQuestionId,
                            QuestionAnswer = options.QuestionAnswer,
                            QuestionAnswerScore = options.QuestionAnswerScore,
                            IsCorrect = options.IsCorrect,
                            IsCandidateAnswered = false,
                            DisplayOrder = options.DisplayOrder
                        };
                        _DbContext.CandidateTestQuestionOptions.Add(candidateTestQuestionOptions);
                    }
                    _DbContext.CandidateTestQuestions.Add(questions);
                }
                _DbContext.SaveChanges(createLog: false);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPaperId"></param>
        /// <returns></returns>
        public TestPapers GetTestPaperById(Guid testPaperId)
        {
            return _DbContext.TestPapers.
                   Include(x => x.LstPaperTechnologies)
                  .Include(x => x.TestLevels)
                  .Include(x => x.LstPaperTechnologies.Select(y => y.Technologies))
                  .Where(x => x.TestPaperId == testPaperId && x.IsDeleted == false)
                  .SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testTockenVerficationViewModel"></param>
        /// <returns></returns>
        public TestInvitations VerifyTestTocken(TestTockenVerficationViewModel testTockenVerficationViewModel)
        {
            return _DbContext.TestInvitations
                   .Where(x => x.TestTocken == testTockenVerficationViewModel.TestTocken && x.IsDeleted == false)
                   .FirstOrDefault();
        }
    }

}
