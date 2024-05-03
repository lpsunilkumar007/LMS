using OnlineTestApp.Domain.Candidate;
using OnlineTestApp.DomainLogic.Admin.ApplyNow;
using OnlineTestApp.ViewModel.ApplyNow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.ApplyNow
{
    public partial class ApplyNowController
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestDetailsId"></param>
        /// <returns></returns>
        //[HttpPost]
        //public JsonResult IsQuestionValidByCandidateTestQuestionId(Guid candidateTestQuestionId)
        //{
        //    if (!Request.IsAjaxRequest()) return null;
        //    ApplyNowDomainLogic obj = new ApplyNowDomainLogic();
        //    return Json(obj.ValidateQuestionByCandidateTestQuestionId(candidateTestQuestionId: candidateTestQuestionId));
        //}



        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="candidateTestDetailsId"></param>
        ///// <returns></returns>
        ///// 
        //[HttpPost]
        //public string IsTestValidByCandidateTestDetailsId(Guid candidateTestDetailsId)
        //{
        //    if (!Request.IsAjaxRequest()) return null;
        //    ApplyNowDomainLogic obj = new ApplyNowDomainLogic();
        //    return obj.ValidateTestByCandidateTestDetailsId(candidateTestDetailsId: candidateTestDetailsId);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id">candidateTestDetailsId</param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult CandidateTestQuestions(Guid id)
        //{
        //    string validateMessage = IsTestValidByCandidateTestDetailsId(id);
        //    if (!string.IsNullOrEmpty(validateMessage))
        //    {
        //        ErrorBlockWithRedirectUrl(validateMessage, "/applynow/applyfortest");
        //    }
        //    return View();
        //}/// <summary>
        // /// 
        // /// </summary>
        // /// <param name="id">candidateTestDetailsId</param>
        // /// <returns></returns>

        //[ChildActionOnly]
        //public ActionResult _CandidateTestTimeDetails(Guid id)
        //{
        //    ApplyNowDomainLogic obj = new ApplyNowDomainLogic();
        //    return PartialView(Task.Run(async () => await obj.GetTestDetailsByCandidateTestDetailsId(id)).Result);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id">candidateTestDetailsId</param>
        ///// <returns></returns>
        //[ChildActionOnly]
        //public ActionResult _CandidateTestQuestionDetails(Guid id)
        //{
        //    string validateMessage = IsTestValidByCandidateTestDetailsId(id);
        //    if (!string.IsNullOrEmpty(validateMessage))
        //    {
        //        ErrorBlock(validateMessage);
        //        ViewBag.GoAway = "/applynow/applyfortest";
        //        //ErrorBlockWithRedirectUrl(validateMessage, ");
        //        return PartialView();
        //    }
        //    ApplyNowDomainLogic obj = new ApplyNowDomainLogic();
        //    ViewBag.CandidateTestDetailsId = id;
        //    //return PartialView(obj.GetTestQuestionsByCandidateTestDetailsId(id));
        //    return PartialView();
        //}


        //[HttpPost]
        //// [ValidateAntiForgeryToken]
        //public async Task<JsonResult> CandidateQuestionDetails(CandidateTestQuestions candidateTestQuestions)
        //{
        //    //if (!Request.IsAjaxRequest()) return null;
        //    //if (string.IsNullOrEmpty(candidateTestQuestions.IsSelectedAnswer))
        //    //{
        //    //    return ReturnAjaxErrorMessage("Please answer this question.");
        //    //}
        //    //ApplyNowDomainLogic obj = new ApplyNowDomainLogic();
        //    //var result = await obj.SubmitCandidateTestQuestionAnswer(candidateTestQuestions.CandidateTestQuestionId, candidateTestQuestions.IsSelectedAnswer);
        //    //if (!result)
        //    //{
        //    //    return ReturnAjaxSuccessMessage("Test Completed", "/applynow/applyfortest");
        //    //}

        //    //string validateMessage = IsTestValidByCandidateTestDetailsId(candidateTestQuestions.FkCandidateAssignedTestId);
        //    //if (!string.IsNullOrEmpty(validateMessage))
        //    //{
        //    //    return ReturnAjaxErrorMessage(validateMessage, "/applynow/applyfortest");
        //    //}
        //    //var nextQuestion = obj.GetTestQuestionsByCandidateTestDetailsId(candidateTestQuestions.FkCandidateAssignedTestId);
        //    //if (nextQuestion == null)
        //    //    return ReturnAjaxSuccessMessage("Test Completed", "/applynow/applyfortest");
        //    //return ReturnAjaxWithSuccessOutPut(this.RenderPartialView("~/views/applynow/_CandidateTestQuestionDetails.cshtml", nextQuestion));

        //    return ReturnAjaxWithSuccessOutPut(this.RenderPartialView("~/views/applynow/_CandidateTestQuestionDetails.cshtml", null));
        //}

        //public async Task<JsonResult> CandidateNextQuestion(CandidateTestQuestions candidateTestQuestions)
        //{           
        //    //ApplyNowDomainLogic obj = new ApplyNowDomainLogic();
        //    //var result = await obj.SubmitCandidateTestQuestionAnswer(candidateTestQuestions.CandidateTestQuestionId, candidateTestQuestions.IsSelectedAnswer);
        //    //if (!result)
        //    //{
        //    //    return ReturnAjaxSuccessMessage("Test Completed", "/applynow/applyfortest");
        //    //}

        //    //string validateMessage = IsTestValidByCandidateTestDetailsId(candidateTestQuestions.FkCandidateAssignedTestId);
        //    //if (!string.IsNullOrEmpty(validateMessage))
        //    //{
        //    //    return ReturnAjaxErrorMessage(validateMessage, "/applynow/applyfortest");
        //    //}
        //    ////var nextQuestion = obj.GetTestQuestionsByCandidateTestDetailsId(candidateTestQuestions.FkCandidateAssignedTestId);
        //    //if (nextQuestion == null)
        //    //    return ReturnAjaxSuccessMessage("Test Completed", "/applynow/applyfortest");
        //   // return ReturnAjaxWithSuccessOutPut(this.RenderPartialView("~/views/applynow/_CandidateTestQuestionDetails.cshtml", nextQuestion));
        //    return ReturnAjaxWithSuccessOutPut(this.RenderPartialView("~/views/applynow/_CandidateTestQuestionDetails.cshtml", null));
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="CandidateTestDetailsId"></param>
        ///// <param name="CandidateTestQuestionId"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<JsonResult> CandidateQuestionSkipped(Guid CandidateTestDetailsId, Guid CandidateTestQuestionId)
        //{
        //    if (!Request.IsAjaxRequest()) return null;
        //    ApplyNowDomainLogic obj = new ApplyNowDomainLogic();
        //    //if (TypeCast.ToType<bool>(IsTimeOut))
        //    //{               
        //    var result = await obj.CandidateQuestionSkipped(CandidateTestQuestionId);
        //    if (!result)
        //    {
        //        return ReturnAjaxSuccessMessage("Test Completed", "/applynow/applyfortest");
        //    }
        //    //}
        //    string validateMessage = IsTestValidByCandidateTestDetailsId(CandidateTestDetailsId);
        //    if (!string.IsNullOrEmpty(validateMessage))
        //    {
        //        return ReturnAjaxErrorMessage(validateMessage, "/applynow/applyfortest");
        //    }
        //    //  var nextQuestion = obj.GetTestQuestionsByCandidateTestDetailsId(CandidateTestDetailsId);
        //    //if (nextQuestion == null)
        //    //    return ReturnAjaxSuccessMessage("Test Completed", "/applynow/applyfortest");

        //    //return ReturnAjaxWithSuccessOutPut(this.RenderPartialView("~/views/applynow/_CandidateTestQuestionDetails.cshtml", nextQuestion));
        //    return ReturnAjaxWithSuccessOutPut(this.RenderPartialView("~/views/applynow/_CandidateTestQuestionDetails.cshtml", null));

        //}


    }
}