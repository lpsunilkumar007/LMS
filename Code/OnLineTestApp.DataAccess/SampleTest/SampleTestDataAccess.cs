using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestApp.Domain.Question;
using System.Data.Entity;

using OnlineTestApp.ViewModel.SampleTest;
using OnlineTestApp.Domain.SampleTest;
using OnlineTestApp.Domain.LookUps;

namespace OnlineTestApp.DataAccess.SampleTest
{
    public class SampleTestDataAccess : BaseClasses.DataAccessBase
    {
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

            List<Questions> questionsList = await (from sampleQuestions in _DbContext.SampleTestQuestions
                                                   where sampleQuestions.FkSampleTestMockUpId == sampleTestMockUpId
                                                   join question in _DbContext.Questions on sampleQuestions.FkQuestionId equals question.QuestionId
                                                   join lv in _DbContext.LookUpDomainValues on sampleQuestions.FkQuestionTechnologyId equals lv.LookUpDomainValueId
                                                   select question).ToListAsync();
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




    }
}
