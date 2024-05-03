using OnlineTestApp.DomainLogic.Admin.Common;
using OnlineTestApp.DomainLogic.Admin.Question;
using OnlineTestApp.ViewModel.Question;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.Question
{
    public partial class QuestionController : BaseClasses.SuperAdminUserControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AddNewQuestion()
        {
            return View(await InitializeQuestion(null));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addEditQuestionViewModel"></param>
        /// <returns></returns>
        async Task<AddEditQuestionViewModel> InitializeQuestion(AddEditQuestionViewModel addEditQuestionViewModel)
        {
            if (addEditQuestionViewModel == null)
            {
                addEditQuestionViewModel = new AddEditQuestionViewModel();
                addEditQuestionViewModel.Questions = new Domain.Question.Questions();
            }
            addEditQuestionViewModel.LstQuestionLevel = await LookUpDomainValuesDomainLogic.GetLookUpDomainValueByLookUpCode(Enums.LookUps.LookUpDomainCode.QuestionLevels);
            addEditQuestionViewModel.LstQuestionTechnology = await LookUpDomainValuesDomainLogic.GetLookUpDomainValueByLookUpCode(Enums.LookUps.LookUpDomainCode.Technology);
            addEditQuestionViewModel.LstQuestionFieldTypes = await QuestionDomainLogic.GetQuestionFieldTypes();

            return addEditQuestionViewModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addEditQuestionViewModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> AddNewQuestion(AddEditQuestionViewModel addEditQuestionViewModel)
        {
            if (!Request.IsAjaxRequest()) return null;

            if (ModelState.IsValid)
            {
                //addEditQuestionViewModel.Questions.LstQuestionOptions.Count() == 0
                if (addEditQuestionViewModel.Questions.LstQuestionOptions.Where(x => x.IsCorrect == true).Count() > 1)
                {
                    addEditQuestionViewModel.Questions.FieldType = Enums.Question.FieldType.CheckBoxList;
                }
                else
                {
                    addEditQuestionViewModel.Questions.FieldType = Enums.Question.FieldType.RadioButtonList;
                }

                ManageQuestionsDomainLogic obj = new ManageQuestionsDomainLogic();
                string result = await obj.AddNewQuestion(addEditQuestionViewModel);
                if (!string.IsNullOrEmpty(result))
                {
                    return ReturnAjaxErrorMessage(result);
                }
                return ReturnAjaxSuccessMessage("Question added successfully");
            }
            return ReturnAjaxModelError();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OptionsData"></param>
        /// <param name="displayOrder"></param>
        /// <returns></returns>
        public ActionResult _AddQuestionOptions(Domain.Question.QuestionOptions OptionsData, int displayOrder = 1)
        {
            //if (!Request.IsAjaxRequest())return null;           

            if (OptionsData == null)
            {
                OptionsData = new Domain.Question.QuestionOptions();
                OptionsData.DisplayOrder = displayOrder;
            }
            if (OptionsData.QuestionOptionId == Guid.Empty)
            {
                OptionsData.QuestionOptionId = Guid.NewGuid();
            }
            ViewBag.Order = displayOrder - 1;
            return PartialView(OptionsData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> EditQuestion(Guid id)
        {
            ManageQuestionsDomainLogic obj = new ManageQuestionsDomainLogic();
            return View(await InitializeQuestion(obj.GetQuestionDetailsById(id)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addEditQuestionViewModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> EditQuestion(AddEditQuestionViewModel addEditQuestionViewModel)
        {
            if (!Request.IsAjaxRequest()) return null;
            if (ModelState.IsValid)
            {
                ManageQuestionsDomainLogic obj = new ManageQuestionsDomainLogic();
                string result = await obj.UpdateQuestionDetails(addEditQuestionViewModel);
                if (!string.IsNullOrEmpty(result))
                {
                    return ReturnAjaxErrorMessage("There should be atleast one answer");
                }
                else
                {
                    return ReturnAjaxSuccessMessage("Question updated successfully");
                }
            }
            return ReturnAjaxModelError();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewQuestionViewModel"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ViewQuestions(ViewQuestionViewModel viewQuestionViewModel)
        {
            ManageQuestionsDomainLogic obj = new ManageQuestionsDomainLogic();
            var result = obj.GetQuestionDetails(viewQuestionViewModel);
            result.LstQuestionLevel = await LookUpDomainValuesDomainLogic.GetLookUpDomainValueByLookUpCode(Enums.LookUps.LookUpDomainCode.QuestionLevels);
            result.LstQuestionTechnology = await LookUpDomainValuesDomainLogic.GetLookUpDomainValueByLookUpCode(Enums.LookUps.LookUpDomainCode.Technology);
            return View(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        [HttpPost]
        public void DeleteQuestion(Guid questionId)
        {
            if (!Request.IsAjaxRequest()) return;
            ManageQuestionsDomainLogic obj = new ManageQuestionsDomainLogic();
            obj.DeleteQuestion(questionId);
        }

    }
}