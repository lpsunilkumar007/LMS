using OnlineTestApp.DataAccess.Question;
using OnlineTestApp.ViewModel.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.Question
{
    public class ManageQuestionsDomainLogic : BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addEditQuestionViewModel"></param>
        /// <returns></returns>
        public async Task<string> AddNewQuestion(AddEditQuestionViewModel addEditQuestionViewModel)
        {           
            addEditQuestionViewModel.Questions.LstQuestionOptions = addEditQuestionViewModel.Questions.LstQuestionOptions.Where(x => x.IsDeleted == false)
                .ToList();
            if(addEditQuestionViewModel.Questions.LstQuestionOptions.Count()==0)
            {
                return "There should be atleast one answer";
            }

            using (ManageQuestionsDataAccess obj = new ManageQuestionsDataAccess())
            {
                obj.AddNewQuestion(addEditQuestionViewModel.Questions);
            }        

            await AssignQuestionLevel(addEditQuestionViewModel.QuestionLevel, addEditQuestionViewModel.Questions.QuestionId, true);

            await AssignQuestionTechnology(addEditQuestionViewModel.QuestionTechnology, addEditQuestionViewModel.Questions.QuestionId, true);
            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionLevel"></param>
        /// <param name="questionId"></param>
        /// <param name="deleteBeforeAssiging"></param>
        /// <returns></returns>
        public async Task AssignQuestionLevel(Guid[] questionLevel, Guid questionId, bool deleteBeforeAssiging)
        {
            List<Domain.Question.QuestionLevel> lstQuestionLevel = new List<Domain.Question.QuestionLevel>();
            foreach (var level in questionLevel)
            {
                lstQuestionLevel.Add(new Domain.Question.QuestionLevel
                {
                    FkQuestionId = questionId,
                    FkQuestionLevel = level,
                });
            }

            using (ManageQuestionsDataAccess obj = new ManageQuestionsDataAccess())
            {
                await obj.AssignQuestionLevel(lstQuestionLevel, questionId, deleteBeforeAssiging);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionTechnology"></param>
        /// <param name="questionId"></param>
        /// <param name="deleteBeforeAssiging"></param>
        /// <returns></returns>
        public async Task AssignQuestionTechnology(Guid[] questionTechnology, Guid questionId, bool deleteBeforeAssiging)
        {
            List<Domain.Question.QuestionTechnology> lstQuestionTechnology = new List<Domain.Question.QuestionTechnology>();
            foreach (var technology in questionTechnology)
            {
                lstQuestionTechnology.Add(new Domain.Question.QuestionTechnology
                {
                    FkQuestionId = questionId,
                    FkQuestionTechnology = technology,
                });
            }

            using (ManageQuestionsDataAccess obj = new ManageQuestionsDataAccess())
            {
                await obj.AssignQuestionTechnology(lstQuestionTechnology, questionId, deleteBeforeAssiging);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public AddEditQuestionViewModel GetQuestionDetailsById(Guid questionId)
        {
            using (ManageQuestionsDataAccess obj = new ManageQuestionsDataAccess())
            {
                return obj.GetQuestionDetailsById(questionId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addEditQuestionViewModel"></param>
        /// <returns></returns>
        public async Task<string> UpdateQuestionDetails(AddEditQuestionViewModel addEditQuestionViewModel)
        {
            if (addEditQuestionViewModel.Questions.LstQuestionOptions.Where(x => x.IsDeleted == false).Count() == 0)                
            {
                return "There should be atleast one answer";
            }

            using (ManageQuestionsDataAccess obj = new ManageQuestionsDataAccess())
            {
                await obj.UpdateQuestionDetails(addEditQuestionViewModel.Questions);
                await obj.UpdateOptionsToQuestion(addEditQuestionViewModel.Questions.LstQuestionOptions, addEditQuestionViewModel.Questions.QuestionId);
            }

            await AssignQuestionLevel(addEditQuestionViewModel.QuestionLevel, addEditQuestionViewModel.Questions.QuestionId, true);

            await AssignQuestionTechnology(addEditQuestionViewModel.QuestionTechnology, addEditQuestionViewModel.Questions.QuestionId, true);

            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        public void DeleteQuestion(Guid questionId)
        {
            using (ManageQuestionsDataAccess obj = new ManageQuestionsDataAccess())
            {
                obj.DeleteQuestion(questionId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewQuestionViewModel"></param>
        /// <returns></returns>
        public ViewQuestionViewModel GetQuestionDetails(ViewQuestionViewModel viewQuestionViewModel)
        {
            switch (viewQuestionViewModel.SortBy)
            {
                case "QuestionTitle asc":
                case "QuestionTitle desc":
                case "FieldType asc":
                case "FieldType desc":
                case "CreatedDateTime asc":
                case "CreatedDateTime desc":
                    break;

                default:
                    viewQuestionViewModel.SortBy = "CreatedDateTime desc";
                    break;
            }
            using (ManageQuestionsDataAccess obj = new ManageQuestionsDataAccess())
            {                
                var result = obj.GetQuestionDetails(viewQuestionViewModel);
                viewQuestionViewModel.QueryString = GetQueryStringsForSorting();
                return result;
            }
        }
    }
}
