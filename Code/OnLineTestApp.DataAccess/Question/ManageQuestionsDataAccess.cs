using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using OnlineTestApp.ViewModel.Question;
using OnlineTestApp.Domain.Question;
using System.Linq.Dynamic;

namespace OnlineTestApp.DataAccess.Question
{
    public class ManageQuestionsDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questions"></param>
        public void AddNewQuestion(Questions questions)
        {
            _DbContext.Questions.Add(questions);
            _DbContext.SaveChanges(createLog: false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstQuestionLevel"></param>
        /// <param name="questionId"></param>
        /// <param name="deleteBeforeAssiging"></param>
        /// <returns></returns>
        public async Task AssignQuestionLevel(List<QuestionLevel> lstQuestionLevel, Guid questionId, bool deleteBeforeAssiging)
        {
            if (deleteBeforeAssiging)
            {
                var existingLevels = _DbContext.QuestionLevel.Where(x => x.FkQuestionId == questionId).ToList();
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
                _DbContext.QuestionLevel.Add(questionLevel);
            }
            await _DbContext.SaveChangesAsync(createLog: true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionOptions"></param>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public async Task UpdateOptionsToQuestion(List<QuestionOptions> questionOptions, Guid questionId)
        {
            var existingOptions = _DbContext.QuestionOptions.Where(x => x.FkQuestionId == questionId).ToList();
            Guid loggedInUserId = UserVariables.LoggedInUserId;
            foreach (var data in questionOptions)
            {
                if (existingOptions.Any(item => item.QuestionOptionId == data.QuestionOptionId))
                {
                    var originalData = await _DbContext.QuestionOptions.Where(x => x.QuestionOptionId == data.QuestionOptionId).SingleAsync();
                    if (data.IsDeleted)
                    {
                        originalData.IsDeleted = true;
                        originalData.DeletedDateDateTime = DateSettings.CurrentDateTime;
                        originalData.FkDeletedBy = loggedInUserId;
                    }
                    else
                    {
                        originalData.QuestionAnswer = data.QuestionAnswer;
                        originalData.QuestionAnswerScore = data.QuestionAnswerScore;
                        originalData.IsCorrect = data.IsCorrect;
                        originalData.DisplayOrder = data.DisplayOrder;
                    }
                    _DbContext.Entry(originalData).State = EntityState.Modified;
                }
                else if (!data.IsDeleted)
                {
                    data.FkQuestionId = questionId;
                    _DbContext.QuestionOptions.Add(data);
                }
            }
            await _DbContext.SaveChangesAsync(createLog: true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstQuestionTechnology"></param>
        /// <param name="questionId"></param>
        /// <param name="deleteBeforeAssiging"></param>
        /// <returns></returns>
        public async Task AssignQuestionTechnology(List<QuestionTechnology> lstQuestionTechnology, Guid questionId, bool deleteBeforeAssiging)
        {
            if (deleteBeforeAssiging)
            {
                var existingQuestions = _DbContext.QuestionTechnology.Where(x => x.FkQuestionId == questionId).ToList();
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
                _DbContext.QuestionTechnology.Add(questionTechnology);
            }
            await _DbContext.SaveChangesAsync(createLog: true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public AddEditQuestionViewModel GetQuestionDetailsById(Guid questionId)
        {   
            var query = _DbContext.Questions
                .Include(x => x.LstQuestionLevel)
                .Include(x => x.LstQuestionTechnology)
                .Include(y => y.LstQuestionOptions)
                .Where(x => x.QuestionId == questionId
                      && x.IsDeleted == false).Single();


            query.LstQuestionOptions = query.LstQuestionOptions.Where(x => x.IsDeleted == false).ToList();

            return new AddEditQuestionViewModel
            {
                Questions = query,
                QuestionLevel = query.LstQuestionLevel.Select(x => x.FkQuestionLevel).ToArray(),
                QuestionTechnology = query.LstQuestionTechnology.Select(x => x.FkQuestionTechnology).ToArray(),
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questions"></param>
        public async Task UpdateQuestionDetails(Questions questions)
        {
            var originalData = _DbContext.Questions.Where(x => x.QuestionId == questions.QuestionId && x.IsDeleted == false).Single();

            originalData.TotalScore = questions.TotalScore;
            originalData.QuestionTitle = questions.QuestionTitle;
            originalData.QuestionDescription = questions.QuestionDescription;
            originalData.TotalTime = questions.TotalTime;
            originalData.CanSkipQuestion = questions.CanSkipQuestion;
            originalData.MaxScore = questions.MaxScore;
            originalData.NegativeMarks = questions.NegativeMarks;
            //originalData.ErrorMessage = questions.ErrorMessage;
            //originalData.RegularExpression = questions.RegularExpression;
           // originalData.ErrorMessageRegularExpression = questions.ErrorMessageRegularExpression;
           // originalData.ValidExtensions = questions.ValidExtensions;
            //originalData.ErrorExtensions = questions.ErrorExtensions;
            originalData.IsActive = questions.IsActive;
            originalData.FieldType = questions.FieldType;

            _DbContext.Entry(originalData).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync(createLog: true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUserId"></param>
        /// <param name="loggedInUserId"></param>
        public void DeleteQuestion(Guid questionId)
        {
            Guid loggedInUserId = UserVariables.LoggedInUserId;
            var originalData = _DbContext.Questions.Where(x => x.QuestionId == questionId && x.IsDeleted == false).Single();
            originalData.IsDeleted = true;
            originalData.DeletedDateDateTime = DateSettings.CurrentDateTime;
            originalData.FkDeletedBy = loggedInUserId;
            _DbContext.Entry(originalData).State = EntityState.Modified;
            _DbContext.SaveChanges(createLog: false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewQuestionViewModel"></param>
        /// <returns></returns>
        public ViewQuestionViewModel GetQuestionDetails(ViewQuestionViewModel viewQuestionViewModel)
        {
            //_SqlParameter = new List<SqlParameter>();
            //_SqlParameter.Add(new SqlParameter("questionTechnology", "B1356190-AC8C-49DD-AEAE-9BDBE0B70F19"));
            //_SqlParameter.Add(new SqlParameter("questionLevel", "E6A48798-95DA-4927-BBA6-80FB612ABE35"));
            //_SqlParameter.Add(new SqlParameter("questionTime", 30));
                      
            //var result = CallStoreProcToList<string>("GetTestQuestions @questionTechnology, @questionLevel, @questionTime");

            var query = _DbContext.Questions.Include(x => x.LstQuestionLevel.Select(y => y.QuestionLevels))
                .Include(x => x.LstQuestionTechnology.Select(y => y.QuestionTechnologies)).Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(viewQuestionViewModel.QuestionTitle))
            {
                query = query.Where(x => x.QuestionTitle.Contains(viewQuestionViewModel.QuestionTitle));
            }

            if (viewQuestionViewModel.QuestionLevel.HasValue)
            {
                query = query.Where(x => x.LstQuestionLevel.Any(y => y.FkQuestionLevel == viewQuestionViewModel.QuestionLevel.Value));
            }

            if (viewQuestionViewModel.QuestionTechnology.HasValue)
            {
                query = query.Where(x => x.LstQuestionTechnology.Any(y => y.FkQuestionTechnology == viewQuestionViewModel.QuestionTechnology.Value));
            }

            viewQuestionViewModel.LstQuestions = query.OrderBy(viewQuestionViewModel.SortBy)
                .Skip(viewQuestionViewModel.SkipRecords)
                .Take(viewQuestionViewModel.PageSize).ToList();
            viewQuestionViewModel.TotalRecords = query.Count();
            return viewQuestionViewModel;
        }
    }
}
