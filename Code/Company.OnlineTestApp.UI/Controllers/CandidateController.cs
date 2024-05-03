using Company.OnlineTestApp.UI.Controllers.Base;
using OnlineTestApp.Domain.Candidate;
using OnlineTestApp.Domain.TestPaper;
using OnlineTestApp.DomainLogic.Company;
using OnlineTestApp.ViewModel.Candidate;
using OnlineTestApp.ViewModel.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Company.OnlineTestApp.UI.Controllers
{
    public class CandidateController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDetailsViewModel"></param>
        /// <returns></returns>
        public ActionResult AddCandidateInfo(TestDetailsViewModel testDetailsViewModel)
        {
            if (!Request.IsAjaxRequest()) { return null; }
            CandidateDomainLogic candidateDomainLogic = new CandidateDomainLogic();
            //if (!candidateDomainLogic.IsTestAttemptedByTestInvitationId(testInvitationId))
            //{
            candidateDomainLogic.AddCandidateInfo(testDetailsViewModel);
            // }
            return Json(new { success = true, data = testDetailsViewModel });
        }

        /// <summary>
        /// For check tocken before start any test 
        /// </summary>
        /// <param name="testTocken"></param>
        /// <returns></returns>
        public ActionResult TestTockenVerification()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testTockenVerficationViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TestTockenVerification(TestTockenVerficationViewModel testTockenVerficationViewModel)
        {
            if (ModelState.IsValid)
            {
                CandidateDomainLogic candidateDomainLogic = new CandidateDomainLogic();
                TestInvitations testInvitations = candidateDomainLogic.VerifyTestTocken(testTockenVerficationViewModel);
                if (testInvitations == null)
                {
                    ErrorBlock("Either you have invalid token or your token has been expired, please contact Adminstrator!");
                }
                else
                {
                    return RedirectToAction("TestDetail", "Candidate", new { FkTestPaperId = testInvitations.FkTestPaperId, TestInvitationId = testInvitations.TestInvitationId });
                }
            }
            return View(testTockenVerficationViewModel);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult _accountVerificationAsside()
        {
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPaper"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult _testDetailAsside(TestPapers testPaper)
        {
            return PartialView(testPaper);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPaperId"></param>
        /// <returns></returns>
        public ActionResult TestDetail(TestInvitations testInvitations)
        {
            CandidateDomainLogic candidateDomainLogic = new CandidateDomainLogic();
            TestDetailsViewModel testPaperViewModel = new TestDetailsViewModel();
            testPaperViewModel.TestPapers = candidateDomainLogic.GetTestPaperById(testInvitations.FkTestPaperId);
            testPaperViewModel.FkTestInvitationId = testInvitations.TestInvitationId;
            return View(testPaperViewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitationId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult StartTest(TestDetailsViewModel testDetailsViewModel)
        {
            if (!Request.IsAjaxRequest()) { return null; }
            CandidateDomainLogic candidateDomainLogic = new CandidateDomainLogic();
            if (!candidateDomainLogic.IsTestAttemptedByTestInvitationId(testDetailsViewModel.FkTestInvitationId))
            {
                candidateDomainLogic.AddCandidateTestQuestionsByTestInvitationId(testDetailsViewModel.FkTestInvitationId);
            }
            return Json(new { success = true, data = testDetailsViewModel });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateTestQuestionId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult IsQuestionValidByCandidateTestQuestionId(Guid candidateTestQuestionId)
        {
            if (!Request.IsAjaxRequest()) return null;
            CandidateDomainLogic obj = new CandidateDomainLogic();
            return Json(obj.ValidateQuestionByCandidateTestQuestionId(candidateTestQuestionId: candidateTestQuestionId));
        }

        /// <summary>
        /// after finishing test 
        /// </summary>
        /// <param name="testInvitationId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FinishTest(CandidateTestQuestions candidateTestQuestions)
        {
            if (!Request.IsAjaxRequest()) { return null; }
            CandidateDomainLogic candidateDomainLogic = new CandidateDomainLogic();
            //if (!candidateDomainLogic.FinishTest(testInvitationId))
            //{
            candidateDomainLogic.FinishTest(candidateTestQuestions.FkTestInvitationId);
            // }
            return ReturnAjaxSuccessMessage("Test Completed", "/applynow/testresult?testInvitationId=" + candidateTestQuestions.FkTestInvitationId);
        }
        /// [HttpGet]
        public ActionResult CandidateTestQuestions(Guid id)
        {
            CandidateDomainLogic candidateDomainLogic = new CandidateDomainLogic();
            string validateMessage = candidateDomainLogic.IsTestValidByTestInvitationId(id);
            if (!string.IsNullOrEmpty(validateMessage))
            {
                return RedirectToAction("TestTockenVerification", "Candidate");
            }
            return View();
        }
        /// <summary>
        /// left panel info when candidate test start 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult _CandidateTestLeftSide(Guid id)
        {
            CandidateDomainLogic candidateDomainLogic = new CandidateDomainLogic();
            return PartialView(candidateDomainLogic.GetCandidateTestQuestionsInfo(id));
        }
        /// <summary>
        /// top of candidate test, display test time information 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult _CandidateTestTimeDetails(Guid id)
        {
            return PartialView();

        }
        /// <summary>
        /// fetch question details 
        /// </summary>
        /// <param name="id">candidateTestDetailsId</param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult _CandidateTestQuestionDetails(Guid id)
        {
            CandidateDomainLogic candidateDomainLogic = new CandidateDomainLogic();
            CandidateTestQuestionsViewModel candidateTestQuestionsViewModel = new CandidateTestQuestionsViewModel
            {
                CandidateTestQuestions = candidateDomainLogic.GetTestQuestionsByTestInvitationId(id),
                IsLastQuestion = candidateDomainLogic.IsLastTestQuestionsByTestInvitationId(id)
            };
            return PartialView(candidateTestQuestionsViewModel);
        }
        /// <summary>
        ///  on question time over fetch next question automatically 
        /// </summary>
        /// <param name="candidateTestQuestions"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CandidateQuestionDetails(CandidateTestQuestions candidateTestQuestions)
        {
            if (!Request.IsAjaxRequest()) return null;
            if (string.IsNullOrEmpty(candidateTestQuestions.IsSelectedAnswer))
            {
                return ReturnAjaxErrorMessage("Please answer this question.");
            }
            CandidateDomainLogic candidateDomainLogic = new CandidateDomainLogic();

            var result = await candidateDomainLogic.SubmitCandidateTestQuestionAnswer(candidateTestQuestions.CandidateTestQuestionId, candidateTestQuestions.IsSelectedAnswer);
            if (!result)
            {
                return ReturnAjaxSuccessMessage("Test Completed", "/Candidate/TestTockenVerification");
            }
            var nextQuestion = candidateDomainLogic.GetTestQuestionsByTestInvitationId(candidateTestQuestions.FkTestInvitationId);
            if (nextQuestion == null)
                return ReturnAjaxSuccessMessage("Test Completed", "/Candidate/TestTockenVerification");

            CandidateTestQuestionsViewModel candidateTestQuestionsViewModel = new CandidateTestQuestionsViewModel
            {
                CandidateTestQuestions = nextQuestion,
                IsLastQuestion = candidateDomainLogic.IsLastTestQuestionsByTestInvitationId(candidateTestQuestions.FkTestInvitationId)
            };

            return ReturnAjaxWithSuccessOutPut(this.RenderPartialView("~/views/Candidate/_CandidateTestQuestionDetails.cshtml", candidateTestQuestionsViewModel));
        }
        /// <summary>
        /// user skip question functioanlity 
        /// </summary>
        /// <param name="TestInvitationId"></param>
        /// <param name="CandidateTestQuestionId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CandidateQuestionSkipped(CandidateTestQuestions candidateTestQuestions)
        {
            if (!Request.IsAjaxRequest()) return null;
            CandidateDomainLogic candidateDomainLogic = new CandidateDomainLogic();
            var result = await candidateDomainLogic.CandidateQuestionSkipped(candidateTestQuestions.CandidateTestQuestionId);
            if (!result)
            {
                return ReturnAjaxSuccessMessage("Test Completed", "/Candidate/TestTockenVerification");
            }
            var nextQuestion = candidateDomainLogic.GetTestQuestionsByTestInvitationId(candidateTestQuestions.FkTestInvitationId);
            if (nextQuestion == null)
                return ReturnAjaxSuccessMessage("Test Completed", "/Candidate/TestTockenVerification");
            CandidateTestQuestionsViewModel candidateTestQuestionsViewModel = new CandidateTestQuestionsViewModel
            {
                CandidateTestQuestions = nextQuestion,
                IsLastQuestion = candidateDomainLogic.IsLastTestQuestionsByTestInvitationId(candidateTestQuestions.FkTestInvitationId)
            };
            return ReturnAjaxWithSuccessOutPut(this.RenderPartialView("~/views/Candidate/_CandidateTestQuestionDetails.cshtml", candidateTestQuestionsViewModel));
        }

        /// <summary>
        /// next button functionality 
        /// </summary>
        /// <param name="candidateTestQuestions"></param>
        /// <returns></returns>
        public JsonResult CandidateNextQuestion(CandidateTestQuestions candidateTestQuestions)
        {
            if (!Request.IsAjaxRequest()) return null;
            CandidateDomainLogic candidateDomainLogic = new CandidateDomainLogic();

            var nextQuestion = candidateDomainLogic.GetTestQuestionsByTestInvitationId(candidateTestQuestions.FkTestInvitationId);
            if (nextQuestion == null)
                return ReturnAjaxSuccessMessage("Test Completed", "/Candidate/TestTockenVerification");
            CandidateTestQuestionsViewModel candidateTestQuestionsViewModel = new CandidateTestQuestionsViewModel
            {
                CandidateTestQuestions = nextQuestion,
                IsLastQuestion = candidateDomainLogic.IsLastTestQuestionsByTestInvitationId(candidateTestQuestions.FkTestInvitationId)
            };
            return ReturnAjaxWithSuccessOutPut(this.RenderPartialView("~/views/Candidate/_CandidateTestQuestionDetails.cshtml", candidateTestQuestionsViewModel));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInvitations"></param>
        /// <returns></returns>
        public ActionResult TestResult(Guid testInvitationId)
        {
            CandidateDomainLogic candidateDomainLogic = new CandidateDomainLogic();
            return View(candidateDomainLogic.GetTestResultById(testInvitationId));
        }
    }
}