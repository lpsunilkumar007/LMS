
using OnlineTestApp.DomainLogic.Admin.Common;
using OnlineTestApp.DomainLogic.Admin.Test;
using OnlineTestApp.ViewModel.Test;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.Test
{
    public class TestController : BaseClasses.AdminUserControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // [HttpGet]
        //public async Task<ActionResult> AddNewTest()
        //{
        //    AddNewTestViewModel prp = new AddNewTestViewModel
        //    {
        //        LstTestLevel = await LookUpDomainValuesDomainLogic.GetLookUpDomainValueByLookUpCode(Enums.LookUps.LookUpDomainCode.QuestionLevels),
        //        LstTestTechnology = await LookUpDomainValuesDomainLogic.GetLookUpDomainValueByLookUpCode(Enums.LookUps.LookUpDomainCode.Technology),
        //        InviteForTest = await EmailDomainLogic.GetEmailTemplateDetailsAssignedToCompany(Enums.Email.EmailTemplateCode.InviteCandidateForTest)
        //    };
        //    ModelState.Clear();
        //    return View(prp);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="addNewTestViewModel"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult GetStep2Data(Guid[] technology, Guid[] level, int totalTime)
        //{
        //    string errors = "";

        //    //var errors = GetDictionaryForAjaxResult;
        //    if (technology == null || technology.Length == 0)
        //    {
        //        errors = "Please select technology\n";
        //    }
        //    if (level == null || level.Length == 0)
        //    {
        //        errors += "Please select level\n";
        //    }
        //    if (totalTime <= 0)
        //    {
        //        errors += "Time cannot zero";
        //    }
        //    if (!string.IsNullOrEmpty(errors))
        //    {
        //        return ReturnAjaxErrorMessage(errors);
        //    }

        //    ManageTestDomainLogic obj = new ManageTestDomainLogic();
        //    return ReturnAjaxWithSuccessOutPut(this.RenderView("~/Views/Test/Step2.cshtml", obj.PrepareTestBatches(technology: technology, level: level, time: totalTime)));
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="prp"></param>
        ///// <returns></returns>
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public async Task<JsonResult> TestQuestionDetails(AddNewTestViewModel prp)
        //{
        //    string emails = Utilities.Validations.IsValidMultipleEmail(prp.InviteForTest.EmailToEmailAddress);
        //    if (emails != string.Empty)
        //    {
        //        ModelState.AddModelError("InValidEmail", "Following email address are wrong : " + emails);
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        ManageTestDomainLogic obj = new ManageTestDomainLogic();
        //        await obj.AddNewTest(prp);
        //        return ReturnAjaxSuccessMessage("New test added successfully", "/test/addnewtest");
        //    }
        //    return ReturnAjaxModelError(); 
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult TestPageDesign()
        //{
        //    return View();
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="viewTestViewModel"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ViewTests(ViewTestViewModel viewTestViewModel)
        //{
        //    ManageTestDomainLogic obj = new ManageTestDomainLogic();
        //    viewTestViewModel.LstTestLevel = await LookUpDomainValuesDomainLogic.GetLookUpDomainValueByLookUpCode(Enums.LookUps.LookUpDomainCode.QuestionLevels);
        //    viewTestViewModel.LstTestTechnology = await LookUpDomainValuesDomainLogic.GetLookUpDomainValueByLookUpCode(Enums.LookUps.LookUpDomainCode.Technology);
        //    return View(await obj.GetTestDetails(viewTestViewModel));
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="testDetailId"></param>
        //[HttpPost]
        //public void DeleteTest(Guid testDetailId)
        //{
        //    if (!Request.IsAjaxRequest()) return;
        //    ManageTestDomainLogic obj = new ManageTestDomainLogic();
        //    obj.DeleteTest(testDetailId);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        public ActionResult EditTest(Guid id)
        {
            ManageTestDomainLogic obj = new ManageTestDomainLogic();
            return View(obj.GetTestDetailsById(id));
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="addEditQuestionViewModel"></param>
        ///// <returns></returns>
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public JsonResult EditTest(Domain.Test.TestDetails testDetails, Guid testDetailId)
        //{
        //    if (!Request.IsAjaxRequest()) return null;
        //    if (ModelState.IsValid)
        //    {
        //        testDetails.TestDetailId = testDetailId;
        //        ManageTestDomainLogic obj = new ManageTestDomainLogic();
        //        obj.UpdateTestDetails(testDetails);
        //        return ReturnAjaxSuccessMessage("Test updated successfully");
        //    }
        //    return ReturnAjaxModelError();
        //}        
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="tempTestId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult PreviewTest(Guid tempTestId)
        //{
        //    ManageTestDomainLogic obj = new ManageTestDomainLogic();
        //    return View(obj.GetPreviewTestDetails(tempTestId));
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="testDetailId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ViewInvitedTestCandidates(ViewTestDetailsViewModel viewTestDetailsViewModel)
        //{
        //    ManageTestDomainLogic obj = new ManageTestDomainLogic();
        //    return View(await obj.GetInvitedTestCandidates(viewTestDetailsViewModel));
        //}
        /////// <summary>
        /////// 
        /////// </summary>
        /////// <param name="id"></param>
        /////// <returns></returns>
        ////public ActionResult InviteAgain(Guid id)
        ////{
        ////    ViewBag.TestDetailsId = id;
        ////    return View();
        ////}
        /////// <summary>
        /////// 
        /////// </summary>
        /////// <param name="inviteForTest"></param>
        /////// <param name="testDetailId"></param>
        /////// <returns></returns>
        ////[HttpPost]
        ////public async Task<JsonResult> InviteAgain(Domain.Email.SendEmail inviteForTest, Guid testDetailId)
        ////{
        ////    if (!Request.IsAjaxRequest()) return null;
        ////    if (ModelState.IsValid)
        ////    {
        ////        DomainLogic.Candidate.AssignTestToCandidateDomainLogic obj = new DomainLogic.Candidate.AssignTestToCandidateDomainLogic();
        ////        await obj.AssignTestToCandidate(inviteForTest, testDetailId);
        ////        return ReturnAjaxSuccessMessage("Candidate invited successfully");
        ////    }
        ////    return ReturnAjaxModelError();
        ////}

        //[HttpGet]
        //public ActionResult InvitedTestCandidateTestResults(Guid candidateTestDetailId)
        //{
        //    ManageTestDomainLogic obj = new ManageTestDomainLogic();
        //    return View(obj.GetInvitedTestCandidateTestResults(candidateTestDetailId));
        //}


        //[HttpPost]
        //public void DeleteInvitedTestCandidateTest(Guid candidateTestDetailId)
        //{
        //    if (!Request.IsAjaxRequest()) return;
        //    ManageTestDomainLogic obj = new ManageTestDomainLogic();
        //    obj.DeleteInvitedTestCandidateTest(candidateTestDetailId);
        //}
    }
}


