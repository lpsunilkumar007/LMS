using OnlineTestApp.DataAccess.SampleTest;
using OnlineTestApp.Domain.Question;
using OnlineTestApp.Domain.SampleTest;
using OnlineTestApp.ViewModel.SampleTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.SampleTest
{
    public class SampleTestDomainLogic : BaseClasses.DomainLogicBase
    {
        public async Task GenerateSampleTest()
        {
            SampleTestDataAccess sampleTestDataAccess = new SampleTestDataAccess();
            Guid[] selectedTechnologies = new Guid[] { new Guid("b699baf7-0b10-4488-bcd2-0c6006cb7bfd") };
            //new Guid("b1356190-ac8c-49dd-aeae-9bdbe0b70f19")
            int selectedDuration = 21;
            int durationPerTechnologies = selectedDuration / selectedTechnologies.Count();
            Guid ExperienceLevel = Guid.Parse("7d438e31-66bb-46cc-8e28-15bc3e5d2282");//Guid.Parse("f188f578-d54e-4e54-864b-0ee7617da400");
            List<Guid> assignedQuestions = new List<Guid>();//to track the assigned questions 
            List<Questions> questionsList = await sampleTestDataAccess.GetQuestionsList(ExperienceLevel, selectedTechnologies);
            string batchName = "Batch-1";

            //for (int stc = 0; stc <= 4; stc++)// for 4 sample test paper 
            //{
            // generate sample paper 
            int sampleTestDuration = 0;
            int sampleTestQuestionsCount = 0;
            decimal sampleTestMarks = 0;
            SampleTestMockups sampleTestMockups = new SampleTestMockups
            {
                SampleTestBatch = batchName,
                Duration = selectedDuration,
                MockupName = "MockUp-5",
                FkTestLevel = ExperienceLevel
            };
            sampleTestMockups = sampleTestDataAccess.SaveSampleTestMockup(sampleTestMockups, selectedTechnologies);
            await AssignTechnologies(selectedTechnologies, sampleTestMockups.SampleTestMockUpId);
            List<SampleTestQuestions> samplestQuestionsList = new List<SampleTestQuestions>();
            int prepareTestCount = 0;

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
                    assignedQuestions.AddRange(sampleTestDataAccess.SaveSampleTestQuestionsList(questionsByTechnologyToSave, sampleTestMockups.SampleTestMockUpId, technology));

                }
            }

            if (sampleTestDuration < selectedDuration && prepareTestCount == 0)
            {
                prepareTestCount++;
                goto prepareTestStart;
            }

            //  prepareTestEnd:
            // update total questions and marks in sample test 
            sampleTestMockups.TotalQuestions = sampleTestQuestionsCount;
            sampleTestMockups.TotalMarks = TypeCast.ToType<int>(sampleTestMarks);
            await sampleTestDataAccess.UpdateSampleTestMockup(sampleTestMockups);
        }

        public async Task<SampleTestViewModel> GetSampleTestById(Guid samplePaperId)
        {
            SampleTestDataAccess sampleTestDataAccess = new SampleTestDataAccess();
            return await sampleTestDataAccess.GetSampleTestById(samplePaperId);
        }




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

            using (SampleTestDataAccess sampleTestDataAccess = new SampleTestDataAccess())
            {
                await sampleTestDataAccess.AssignTechnologies(lstTestTechnologies);
            }
        }
    }
}
