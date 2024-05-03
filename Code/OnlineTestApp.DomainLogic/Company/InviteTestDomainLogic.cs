using OnlineTestApp.DataAccess.Company;
using OnlineTestApp.Domain.Candidate;
using OnlineTestApp.Domain.Email;
using OnlineTestApp.Domain.Question;
using OnlineTestApp.Domain.SampleTest;
using OnlineTestApp.Domain.TestPaper;
using OnlineTestApp.DomainLogic.Admin.BaseClasses;
using OnlineTestApp.Enums.Email;
using OnlineTestApp.ViewModel.Company;
using OnlineTestApp.ViewModel.SampleTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.EmailSender;
using Utilities.EmailSender.Domain;

namespace OnlineTestApp.DomainLogic.Company
{
    public class InviteTestDomainLogic : DomainLogicBase
    {
        #region Invite Test
        public static Domain.User.ApplicationUsers GetUserDetailsForTopMenu()
        {
            using (InviteTestDataAccess obj = new InviteTestDataAccess())
            {
                return obj.GetUserDetailsForTopMenu(UserVariables.LoggedInUserId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTestId"></param>
        /// <returns></returns>
        public async Task<TestPapers> PrepareTestPaper(Guid sampleTestId)
        {
            InviteTestDataAccess inviteTestDataAccess = new InviteTestDataAccess();
            SampleTestMockups sampleTestMockupDetails = //await 
                inviteTestDataAccess.GetSampleTestDetailsById(sampleTestId);
            TestPapers testPaper = new TestPapers
            {
                SampleTestBatch = sampleTestMockupDetails.SampleTestBatch,
                Duration = sampleTestMockupDetails.Duration,
                MockupName = sampleTestMockupDetails.MockupName,
                TotalMarks = sampleTestMockupDetails.TotalMarks,
                TotalQuestions = sampleTestMockupDetails.TotalQuestions,
                FkTestLevel = sampleTestMockupDetails.FkTestLevel,
                IsNegativeMarking = sampleTestMockupDetails.IsNegativeMarking,
                PassingPercentage = sampleTestMockupDetails.PassingPercentage
            };
            inviteTestDataAccess.SaveTestPaper(testPaper);
            await inviteTestDataAccess.AssignTechnologiesToTestPaper(sampleTestId, testPaper.TestPaperId);
            await inviteTestDataAccess.AssignQuestionsTestPaper(sampleTestId, testPaper.TestPaperId);
            return testPaper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<SampleTestMockups>> GenerateSampleTest(Guid[] selectedTechnologies, int selectedDuration, Guid ExperienceLevel, bool isNagativeMarking)
        {
            List<SampleTestMockups> testLst = new List<SampleTestMockups>();
            InviteTestDataAccess inviteTestDataAccess = new InviteTestDataAccess();
            int durationPerTechnologies = selectedDuration / selectedTechnologies.Count();
            List<Guid> assignedQuestions = new List<Guid>();//to track the assigned questions 
            List<Questions> questionsList = await inviteTestDataAccess.GetQuestionsList(ExperienceLevel, selectedTechnologies);

            //
            List<int> technologyDurationList = new List<int>(); // group by 
            foreach (var technology in selectedTechnologies)
            {
                int technologyQuestionsDuration = questionsList.Where(x => x.LstQuestionTechnology.Any(y => y.FkQuestionTechnology == technology)).Sum(x => x.TotalTime);
                technologyDurationList.Add(technologyQuestionsDuration);
            }

            // for more than one modal test paper
            int db_minDurationTechnologiesQuesDur = technologyDurationList.Min();
            decimal totalMockups_decimal = db_minDurationTechnologiesQuesDur / durationPerTechnologies;
            int totalMockups = (int)Math.Round(totalMockups_decimal) == 0 ? 1 : (int)Math.Round(totalMockups_decimal);
            totalMockups = totalMockups > 4 ? 4 : totalMockups;

            string batchName = "Batch-" + Guid.NewGuid();
            string mockUpName = await inviteTestDataAccess.MockUpname(ExperienceLevel, selectedTechnologies);
            int sampleTC = 0;

            for (int stc = 1; stc <= totalMockups; stc++)// mockup  creation loop start ===
            {
                sampleTC++;
               
                List<Guid> assignedSampleQuestions = new List<Guid>();//to track the assigned questions  in sample test 
                
                // generate sample paper 
                int sampleTestDuration = 0;
                int sampleTestQuestionsCount = 0;
                decimal sampleTestMarks = 0;
                SampleTestMockups sampleTestMockups = new SampleTestMockups
                {
                    SampleTestBatch = batchName,
                    Duration = selectedDuration,
                    MockupName = mockUpName,
                    FkTestLevel = ExperienceLevel,
                    IsNegativeMarking = isNagativeMarking,
                    PassingPercentage = Utilities.AppSettings.GetIntValue("PassingPercentage")
                };
                sampleTestMockups = inviteTestDataAccess.SaveSampleTestMockup(sampleTestMockups, selectedTechnologies);
                await AssignTechnologies(selectedTechnologies, sampleTestMockups.SampleTestMockUpId);
                List<SampleTestQuestions> samplestQuestionsList = new List<SampleTestQuestions>();
                int prepareTestCount = 0;


                // to prepare test logic start from here 
                prepareTestStart:
                if (prepareTestCount == 1)
                {
                    durationPerTechnologies = selectedDuration - sampleTestDuration;
                }
                foreach (var technology in selectedTechnologies)
                {
                    List<Questions> questionsByTechnology = questionsList.Where(x => x.LstQuestionTechnology.Any(y => y.FkQuestionTechnology == technology && !assignedQuestions.Contains(y.FkQuestionId))).ToList();
                    List<Questions> questionsByTechnologyToSave = new List<Questions>();
                    int _technologyDuration = 0;
                    foreach (var question in questionsByTechnology)
                    {
                        if (sampleTestDuration == selectedDuration)
                        {
                            break;
                        }
                        if ((_technologyDuration + question.TotalTime) <= durationPerTechnologies && (sampleTestDuration + question.TotalTime) <= selectedDuration)
                        {
                            sampleTestMarks = sampleTestMarks + question.TotalScore;
                            sampleTestQuestionsCount++;
                            sampleTestDuration = sampleTestDuration + question.TotalTime;
                            _technologyDuration = _technologyDuration + question.TotalTime;
                            questionsByTechnologyToSave.Add(question);

                        }
                    }

                    if (questionsByTechnologyToSave.Count > 0)
                    {
                        assignedQuestions.AddRange(inviteTestDataAccess.SaveSampleTestQuestionsList(questionsByTechnologyToSave, sampleTestMockups.SampleTestMockUpId, technology));
                        assignedSampleQuestions = questionsByTechnologyToSave.Select(x => x.QuestionId).ToList();
                    }
                }

                if (sampleTestDuration < selectedDuration && prepareTestCount == 0)
                {
                    prepareTestCount++;
                    goto prepareTestStart;
                }

                // check here for last mockup and add  test questions by slecting from previous sample papers  
                if (totalMockups == sampleTC && sampleTestDuration != selectedDuration)
                {
                    List<Questions> questionsListAssigned = (from q in questionsList
                                                             join r in assignedQuestions on
                                                              q.QuestionId equals r
                                                             select q).ToList();
                    questionsListAssigned = questionsListAssigned.Where(p => !assignedSampleQuestions.Any(p2 => p2 == p.QuestionId)).ToList();
                    //1
                    foreach (var question in questionsListAssigned)
                    {
                        if (sampleTestDuration == selectedDuration)
                        {
                            break;
                        }
                        //1  if ((sampleTestDuration + question.TotalTime) <= selectedDuration && !assignedSampleQuestions.Contains(question.QuestionId))

                        if ((sampleTestDuration + question.TotalTime) <= selectedDuration)
                        {
                            sampleTestMarks = sampleTestMarks + question.TotalScore;
                            sampleTestQuestionsCount++;
                            sampleTestDuration = sampleTestDuration + question.TotalTime;
                            Guid technology = question.LstQuestionTechnology.FirstOrDefault().FkQuestionTechnology;
                            assignedSampleQuestions.Add(inviteTestDataAccess.SaveSampleTestQuestion(question, sampleTestMockups.SampleTestMockUpId, technology));
                        }
                    }
                }
                //  prepareTestEnd:


                // update total questions and marks in sample test 
                sampleTestMockups.TotalQuestions = sampleTestQuestionsCount;
                sampleTestMockups.TotalMarks = TypeCast.ToType<int>(sampleTestMarks);
                await inviteTestDataAccess.UpdateSampleTestMockup(sampleTestMockups);
                testLst.Add(sampleTestMockups);
            }
            return testLst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedTechnologies"></param>
        /// <param name="SampleTestMockUpId"></param>
        /// <returns></returns>
        public async Task AssignTechnologies(Guid[] selectedTechnologies, Guid SampleTestMockUpId)
        {
            List<SampleTestTechnologies> lstTestTechnologies = new List<SampleTestTechnologies>();
            foreach (var technology in selectedTechnologies)
            {
                lstTestTechnologies.Add(new SampleTestTechnologies
                {
                    FkSampleTestMockUpId = SampleTestMockUpId,
                    FkTechnology = technology
                });
            }

            using (InviteTestDataAccess inviteTestDataAccess = new InviteTestDataAccess())
            {
                await inviteTestDataAccess.AssignTechnologies(lstTestTechnologies);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="samplePaperId"></param>
        /// <returns></returns>
        public SampleTestMockups GetSampleTestDetailsById(Guid samplePaperId)
        {
            InviteTestDataAccess inviteTestDataAccess = new InviteTestDataAccess();
            return inviteTestDataAccess.GetSampleTestDetailsById(samplePaperId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTestMockUpId"></param>
        /// <returns></returns>
        public async Task<SampleTestViewModel> GetSampleTestById(Guid samplePaperId)
        {
            InviteTestDataAccess inviteTestDataAccess = new InviteTestDataAccess();
            return await inviteTestDataAccess.GetSampleTestById(samplePaperId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendTestInvitationViewModel"></param>
        /// <returns></returns>     

        public async Task SendTestInvitation(TestInvitationViewModel sendTestInvitationViewModel)
        {
            InviteTestDataAccess inviteTestDataAccess = new InviteTestDataAccess();
            List<TestInvitations> invitedCandidateList = //await 
                inviteTestDataAccess.SendTestInvitation(sendTestInvitationViewModel);
            await SendEmailsToCandidates(invitedCandidateList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invitedCandidateList"></param>
        /// <returns></returns>
        public async Task SendEmailsToCandidates(List<TestInvitations> invitedCandidateList)
        {
            InviteTestDataAccess inviteTestDataAccess = new InviteTestDataAccess();
            EmailDomain emailDomain = new EmailDomain();
            EmailTemplates emailTemplates = await inviteTestDataAccess.GetEmailTemplateByCode(EmailTemplateCode.InviteCandidateForTest);

            foreach (var item in invitedCandidateList)
            {
                emailDomain.EmailTo = item.Email;
                emailDomain.EmailFrom = emailTemplates.EmailFromEmailAddress;
                emailDomain.EmailSubject = emailTemplates.EmailSubject;
                string emailBody = emailTemplates.EmailBody.Replace("tockenno", item.TestTocken);
                emailDomain.EmailBody = emailBody;
                await EmailSender.SendEmail(emailDomain);
            }
        }
        #endregion

        #region dashborad and report 
        public async Task<CandidateTestReportsViewModel> GetTestReports()
        {
            InviteTestDataAccess inviteTestDataAccess = new InviteTestDataAccess();
            return await inviteTestDataAccess.GetReports();

        }
        public async Task<List<TestInvitations>> GetCandidatesResultbyTestpaperId(Guid testPaperId, int retrievePapers)
        {
            InviteTestDataAccess inviteTestDataAccess = new InviteTestDataAccess();
            return await inviteTestDataAccess.GetCandidatesResultbyTestpaperId(testPaperId, retrievePapers);

        }
        public TestInvitations GetInvitedTestCandidateTestResults(Guid testInvitationId)
        {
            using (InviteTestDataAccess obj = new InviteTestDataAccess())
            {
                return obj.GetInvitedTestCandidateTestResults(testInvitationId);
            }
        }
        #endregion
    }
}
