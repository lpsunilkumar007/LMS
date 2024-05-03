using OnlineTestApp.DomainLogic.Admin.ApplyNow;
using OnlineTestApp.ViewModel.ApplyNow;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.ApplyNow
{
    public partial class ApplyNowController : BaseClasses.AnonymousUserControllerBase
    {
        //#region Step 1
        ///// <summary>
        ///// Ste 1
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult ApplyForTest(string id)
        //{
        //    return View(new Step1CheckReferenceViewModel
        //    {
        //        ReferenceNumber = id
        //    });
        //}
        ///// <summary>
        ///// Step 1
        ///// </summary>
        ///// <param name="step1CheckReferenceViewModel"></param>
        ///// <returns></returns>
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public JsonResult ApplyForTest(Step1CheckReferenceViewModel step1CheckReferenceViewModel)
        //{
        //    string validateMessage = IsTestValidByReferenceNumber(step1CheckReferenceViewModel.ReferenceNumber);
        //    if (string.IsNullOrEmpty(validateMessage))
        //    {
        //        return ReturnAjaxSuccessMessage("", "/applynow/candidatetestdetails?referenceNumber=" + step1CheckReferenceViewModel.ReferenceNumber);
        //    }
        //    return ReturnAjaxErrorMessage(validateMessage);
        //}
        //#endregion Step 1
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="referenceNumber"></param>
        ///// <returns></returns>
        //string IsTestValidByReferenceNumber(string referenceNumber)
        //{
        //    ApplyNowDomainLogic obj = new ApplyNowDomainLogic();
        //    return obj.ValidateTestByReferenceNumber(referenceNumber);
        //}

        //#region Step 2

        ///// <summary>
        ///// Step 2
        ///// </summary>
        ///// <param name="referenceNumber"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult CandidateTestDetails(string referenceNumber)
        //{
        //    //validate test
        //    string validateMessage = IsTestValidByReferenceNumber(referenceNumber);
        //    if (!string.IsNullOrEmpty(validateMessage))
        //    {
        //        ErrorBlockWithRedirectUrl(validateMessage, "/applynow/applyfortest");
        //    }
        //    ApplyNowDomainLogic obj = new ApplyNowDomainLogic();
        //    return View(obj.GetTestDetailsByReferenceNumber(referenceNumber));
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="step2ConfirmationViewModel"></param>
        ///// <param name="testReferenceNumber"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<JsonResult> CandidateTestDetails(Step2ConfirmationViewModel step2ConfirmationViewModel)
        //{           
        //    string validateMessage = IsTestValidByReferenceNumber(step2ConfirmationViewModel.ReferenceNumber);
        //    if (!string.IsNullOrEmpty(validateMessage))
        //    {
        //        return ReturnAjaxErrorMessage(validateMessage, "/applynow/applyfortest");
        //    }
        //    ApplyNowDomainLogic obj = new ApplyNowDomainLogic();
        //    return ReturnAjaxSuccessMessage("", "/applynow/CandidateTestQuestions/" + await obj.AssignTestToCandidate(step2ConfirmationViewModel.TestDetails.TestDetailId, step2ConfirmationViewModel.CandidateAssignedId));
        //}

        //#endregion Step 2


    }
}