using OnlineTestApp.Domain.Email;
using OnlineTestApp.Domain.LookUps;
using OnlineTestApp.Domain.Question;
using OnlineTestApp.Domain.SampleTest;
using OnlineTestApp.Domain.TestPaper;
using OnlineTestApp.ViewModel.Company;
using OnlineTestApp.ViewModel.SampleTest;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.DataAccess.Company
{
    public class InviteTestDataAccess : BaseClasses.DataAccessBase
    {

        #region Invite Test
        public Domain.User.ApplicationUsers GetUserDetailsForTopMenu(Guid applicationUserId)
        {
            return _DbContext.ApplicationUsers
                .Include(x => x.ApplicationUserSettings)
                .Where(x => x.IsDeleted == false && x.ApplicationUserId == applicationUserId).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmailTemplateCode"></param>
        /// <returns></returns>
        public async Task<EmailTemplates> GetEmailTemplateByCode(Enums.Email.EmailTemplateCode? EmailTemplateCode)
        {
            EmailTemplates emailTemplate = await _DbContext.EmailTemplates.Where(x => x.EmailTemplateCode.Value == EmailTemplateCode && x.IsDeleted == false).FirstOrDefaultAsync();
            return emailTemplate;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExperienceLevel"></param>
        /// <param name="selectedTechnologies"></param>
        /// <returns></returns>
        public async Task<string> MockUpname(Guid ExperienceLevel, Guid[] selectedTechnologies)
        {
            string mockupName = "";
            List<LookUpDomainValues> technologies = await _DbContext.LookUpDomainValues.Where(d => selectedTechnologies.Contains(d.LookUpDomainValueId))
               .ToListAsync();


            LookUpDomainValues experience = await _DbContext.LookUpDomainValues.Where(x => x.LookUpDomainValueId == ExperienceLevel).FirstOrDefaultAsync();
            foreach (var technology in technologies)
            {
                mockupName = mockupName + " " + technology.LookUpDomainValueText;
            }
            mockupName = mockupName + " " + experience.LookUpDomainValueText + " " + DateSettings.CurrentDateTime.ToString("MMMM dd, yyyy");

            return mockupName;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExperienceLevel"></param>
        /// <param name="selectedTechnologies"></param>
        /// <returns></returns>
        public async Task<List<Questions>> GetQuestionsList(Guid ExperienceLevel, Guid[] selectedTechnologies)
        {
            List<Questions> questionsByExperience = await (from questions in _DbContext.Questions
                                                           join questionLevel in _DbContext.QuestionLevel on questions.QuestionId equals questionLevel.FkQuestionId
                                                           where questionLevel.FkQuestionLevel == ExperienceLevel && questions.IsDeleted == false
                                                           select questions).OrderBy(r => Guid.NewGuid()).ToListAsync();

            List<QuestionTechnology> questionsByTechnology = await _DbContext.QuestionTechnology.Where(x => selectedTechnologies.Contains(x.FkQuestionTechnology)).ToListAsync();

            return (from qByExperience in questionsByExperience
                    join qByTechnology in questionsByTechnology on qByExperience.QuestionId equals qByTechnology.FkQuestionId
                    select qByExperience
                    ).Distinct().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTestMockups"></param>
        /// <returns></returns>
        public SampleTestMockups SaveSampleTestMockup(SampleTestMockups sampleTestMockups, Guid[] selectedTechnologies)
        {
            _DbContext.SampleTestMockups.Add(sampleTestMockups);
            _DbContext.SaveChanges(createLog: false);
            return sampleTestMockups;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPapers"></param>
        /// <returns></returns>
        public TestPapers SaveTestPaper(TestPapers testPapers)
        {
            _DbContext.TestPapers.Add(testPapers);
            _DbContext.SaveChanges(createLog: false);
            return testPapers;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTestId"></param>
        /// <param name="testPaperId"></param>
        /// <returns></returns>

        public async Task AssignQuestionsTestPaper(Guid sampleTestId, Guid testPaperId)
        {
            List<SampleTestQuestions> questionsList = (from question in _DbContext.SampleTestQuestions
                                                       where question.FkSampleTestMockUpId == sampleTestId
                                                       select question).ToList();
            //adding questions to  Test 
            foreach (var question in questionsList)
            {
                TestPaperQuestions testPaperQuestions = new TestPaperQuestions
                {
                    FkTestPaperId = testPaperId,
                    FkQuestionId = question.FkQuestionId,
                    FkQuestionTechnologyId = question.FkQuestionTechnologyId,
                };
                _DbContext.TestPaperQuestions.Add(testPaperQuestions);
            }
            await _DbContext.SaveChangesAsync(createLog: true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTestId"></param>
        /// <param name="testPaperId"></param>
        /// <returns></returns>
        public async Task AssignTechnologiesToTestPaper(Guid sampleTestId, Guid testPaperId)
        {
            List<SampleTestTechnologies> sampleTestTechnologies = (from tech in _DbContext.SampleTestTechnologies
                                                                   where tech.FkSampleTestMockUpId == sampleTestId
                                                                   select tech).ToList();
            //adding technologies to  Test 
            foreach (var technology in sampleTestTechnologies)
            {
                TestPaperTechnologies testPaperTechnologies = new TestPaperTechnologies
                {
                    FkTestPaperId = testPaperId,
                    FkTechnology = technology.FkTechnology,

                };
                _DbContext.TestPaperTechnologies.Add(testPaperTechnologies);
            }
            await _DbContext.SaveChangesAsync(createLog: true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstTestTechnologies"></param>
        /// <returns></returns>
        public async Task AssignTechnologies(List<SampleTestTechnologies> lstTestTechnologies)
        {
            //adding technologies to Sample Test 
            foreach (var technology in lstTestTechnologies)
            {
                _DbContext.SampleTestTechnologies.Add(technology);
            }
            await _DbContext.SaveChangesAsync(createLog: true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTestMockups"></param>
        public async Task UpdateSampleTestMockup(SampleTestMockups sampleTestMockups)
        {
            var originalData = _DbContext.SampleTestMockups.Where(x => x.SampleTestMockUpId == sampleTestMockups.SampleTestMockUpId && x.IsDeleted == false).Single();
            originalData.TotalQuestions = sampleTestMockups.TotalQuestions;
            originalData.TotalMarks = sampleTestMockups.TotalMarks;
            _DbContext.Entry(originalData).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync(createLog: true);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionslist"></param>
        /// <param name="sampleTestMockUpId"></param>
        /// <param name="questionTechnologyId"></param>
        /// <returns></returns>
        public List<Guid> SaveSampleTestQuestionsList(List<Questions> questionslist, Guid sampleTestMockUpId, Guid questionTechnologyId)
        {
            //int totalquestionsduration = 0;
            List<Guid> AssignedQuestionsList = new List<Guid>();
            foreach (var question in questionslist)
            {
                AssignedQuestionsList.Add(question.QuestionId);
                SampleTestQuestions sampleTestQuestions = new SampleTestQuestions
                {
                    FkQuestionTechnologyId = questionTechnologyId,
                    FkSampleTestMockUpId = sampleTestMockUpId,
                    FkQuestionId = question.QuestionId
                };
                _DbContext.SampleTestQuestions.Add(sampleTestQuestions);
                _DbContext.SaveChanges(createLog: false);
            }
            return AssignedQuestionsList;
        }

        /// <summary>
        ///  to assign single question to sample paper 
        /// </summary>
        /// <param name="question"></param>
        /// <param name="sampleTestMockUpId"></param>
        /// <param name="questionTechnologyId"></param>
        /// <returns></returns>
        public Guid SaveSampleTestQuestion(Questions question, Guid sampleTestMockUpId, Guid questionTechnologyId)
        {
            SampleTestQuestions sampleTestQuestions = new SampleTestQuestions
            {
                FkQuestionTechnologyId = questionTechnologyId,
                FkSampleTestMockUpId = sampleTestMockUpId,
                FkQuestionId = question.QuestionId
            };
            _DbContext.SampleTestQuestions.Add(sampleTestQuestions);
            _DbContext.SaveChanges(createLog: false);

            return question.QuestionId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTestMockUpId"></param>
        /// <returns></returns>
        public async Task<SampleTestViewModel> GetSampleTestById(Guid sampleTestMockUpId)
        {
            SampleTestViewModel sampleTestViewModel = new SampleTestViewModel();
            SampleTestMockups sampleTestMockups = await (from samplePaper in _DbContext.SampleTestMockups
                                                         where samplePaper.SampleTestMockUpId == sampleTestMockUpId
                                                         select samplePaper).FirstOrDefaultAsync();
            sampleTestViewModel.SampleTestMockups = sampleTestMockups;
            List<Questions> questionsList = await
                                                 (
                                                 from sampleQuestions in _DbContext.SampleTestQuestions
                                                 where sampleQuestions.FkSampleTestMockUpId == sampleTestMockUpId
                                                 join question in _DbContext.Questions on sampleQuestions.FkQuestionId equals question.QuestionId
                                                 join lv in _DbContext.LookUpDomainValues on sampleQuestions.FkQuestionTechnologyId equals lv.LookUpDomainValueId
                                                 join qop in _DbContext.QuestionOptions on sampleQuestions.FkQuestionId equals qop.FkQuestionId
                                                 select question
                                                   ).ToListAsync();
            //List<Questions> questionsList = await
            //                                   (
            //                                   _DbContext.SampleTestQuestions.Join(_DbContext.Questions, x => x.FkQuestionId, y => y.QuestionId, (x, y) => new
            //                                   {
            //                                       sampleTestQuestions = x,
            //                                       questions = y,
            //                                   })
            //                                    .Include(x => x.questions.LstQuestionOptions)
            //                                    .Include(x => x.questions.LstQuestionTechnology)
            //                                   .Include(questions => questions.questions.LstQuestionTechnology.Select(y => y.QuestionTechnologies.LookUpDomainCode))


            //                                   .Include(x => x.questions.LstQuestionOptions.Select(y => y.QuestionAnswer))
            //                                   .Where(x => x.sampleTestQuestions.FkSampleTestMockUpId == sampleTestMockUpId)
            //                                   .Select(x => x.questions)

            //                                     ).ToListAsync();

            sampleTestViewModel.Questions = questionsList;

            List<SampleTestQuestions> sampleTextQuestionsList = await (from sampleQuestions in _DbContext.SampleTestQuestions
                                                                       where sampleQuestions.FkSampleTestMockUpId == sampleTestMockUpId
                                                                       select sampleQuestions).ToListAsync();
            List<LookUpDomainValues> lookUpDomainValueslist = await (from lv in _DbContext.LookUpDomainValues
                                                                     select lv).ToListAsync();
            sampleTestViewModel.SampleTestQuestions = sampleTextQuestionsList;
            sampleTestViewModel.LookUpDomainValues = lookUpDomainValueslist;
            return sampleTestViewModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTestMockUpId"></param>
        /// <returns></returns>
        public SampleTestMockups GetSampleTestDetailsById(Guid sampleTestMockUpId)
        {
            SampleTestMockups sampleTestMockups = (from samplePaper in _DbContext.SampleTestMockups
                                                   where samplePaper.SampleTestMockUpId == sampleTestMockUpId
                                                   select samplePaper).FirstOrDefault();
            return sampleTestMockups;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationViewModel"></param>
        /// <returns></returns>
        public List<TestInvitations> SendTestInvitation(TestInvitationViewModel testInvitationViewModel)
        {
            TestPapers testPaper = _DbContext.TestPapers.Where(x => x.TestPaperId == testInvitationViewModel.FkTestPaperId).FirstOrDefault();
            List<TestInvitations> InvitedCandidatelist = new List<TestInvitations>();
            if (testPaper != null)
            {
                var emails = testInvitationViewModel.EmailFromEmailAddress.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct();



                Random random = new Random();
                foreach (var email in emails)
                {
                    var testInvite = _DbContext.TestInvitations.Where(x => x.FkTestPaperId == testInvitationViewModel.FkTestPaperId && x.Email == email).FirstOrDefault();
                    if (testInvite == null)
                    {
                        string value = random.Next(10000).ToString();
                        TestInvitations testInvitation = new TestInvitations
                        {
                            FkTestPaperId = testInvitationViewModel.FkTestPaperId,
                            Email = email,
                            TestTocken = value,
                            TotalQuestions = testPaper.TotalQuestions,
                            TestName = testPaper.MockupName,
                            IsNegativeMarking = testPaper.IsNegativeMarking,
                            PassingPercentage = testPaper.PassingPercentage,
                            TotalScore = testPaper.TotalMarks
                        };
                        InvitedCandidatelist.Add(testInvitation);
                        _DbContext.TestInvitations.Add(testInvitation);
                    }

                }
                _DbContext.SaveChanges(createLog: true);
            }
            return InvitedCandidatelist;
        }

        #endregion

        #region Dashboard and report 
        public async Task<CandidateTestReportsViewModel> GetReports()
        {
            CandidateTestReportsViewModel candidateTestReportsViewModel = new CandidateTestReportsViewModel();
            candidateTestReportsViewModel.LstTestPaperReport = await _DbContext.TestInvitations.Where(x => x.IsDeleted == false)
                //.Include(x=>x.TestPaper)
                .GroupBy(t => t.FkTestPaperId)
                .Select(g => new TestPaperReport
                {
                    TestpaperId = g.Key,
                    TestName = g.FirstOrDefault().TestName,
                    TotalAttempted = g.Where(x => x.IsAttempted == true  && x.IsTestFinished == true).Count(),
                    Total = g.Where(x => x.IsDeleted == false).Count(),
                    TotalPassed = g.Where(x => x.IsDeleted == false && x.IsPassed == true && x.IsAttempted == true&& x.IsTestFinished==true).Count(),
                    TotalFail = g.Where(x => x.IsDeleted == false && x.IsPassed == false && x.IsAttempted == true && x.IsTestFinished == true).Count(),

                }).OrderBy(x => x.TestName).ToListAsync();
            return candidateTestReportsViewModel;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPaperId"></param>
        /// <param name="retrievePapers"></param>
        /// <returns></returns>
        public async Task<List<TestInvitations>> GetCandidatesResultbyTestpaperId(Guid testPaperId, int retrievePapers)
        {
            if (retrievePapers == 1)
            {
                return await _DbContext.TestInvitations.Where(x => x.FkTestPaperId == testPaperId && x.IsDeleted == false ).ToListAsync();
                //&& x.IsTestFinished == true
            }
            if (retrievePapers == 2)
            {
                return await _DbContext.TestInvitations.Where(x => x.FkTestPaperId == testPaperId && x.IsDeleted == false && x.IsTestFinished == true && x.IsPassed == true).ToListAsync();
            }

            if (retrievePapers == 4)
            {
                return await _DbContext.TestInvitations.Where(x => x.FkTestPaperId == testPaperId && x.IsDeleted == false && x.IsTestFinished == true && x.IsAttempted == true).ToListAsync();
            }
            return await _DbContext.TestInvitations.Where(x => x.FkTestPaperId == testPaperId && x.IsDeleted == false && x.IsTestFinished == true && x.IsPassed == false).ToListAsync();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationId"></param>
        /// <returns></returns>
        public TestInvitations GetInvitedTestCandidateTestResults(Guid testInvitationId)
        {
            return _DbContext.TestInvitations
                 .Include(x => x.LstCandidateTestQuestions)
                 .Include(x => x.LstCandidateTestQuestions.Select(y => y.LstCandidateTestQuestionOptions))
                 .Where(x => x.TestInvitationId == testInvitationId && x.IsDeleted == false).Single();
        }

        #endregion
    }
}
