using Company.OnlineTestApp.UI.Controllers.Base;
using OnlineTestApp.Domain.SampleTest;
using OnlineTestApp.Domain.TestPaper;
using OnlineTestApp.DomainLogic.Admin.Common;
using OnlineTestApp.DomainLogic.Company;
using OnlineTestApp.Enums.LookUps;
using OnlineTestApp.ViewModel.Company;
using OnlineTestApp.ViewModel.SampleTest;
using OnlineTestApp.ViewModel.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Company.OnlineTestApp.UI.Controllers
{
    public class InviteTestController : AuthorizeController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        //[ChildActionOnly]
        //public ActionResult _topMenu()
        //{

        //    return PartialView(InviteTestDomainLogic.GetUserDetailsForTopMenu());
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: InviteTest
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult _ExperienceLevels()
        {
            TestMockupViewModel testMockupViewModel = new TestMockupViewModel
            {
                LstExperienceLevel = Task.Run(async () => await LookUpDomainValuesDomainLogic.GetLookUpDomainValueByLookUpCode(LookUpDomainCode.QuestionLevels)).Result,

            };
            ModelState.Clear();

            return PartialView(testMockupViewModel);
        }


        [ChildActionOnly]
        public ActionResult _Technologies()
        {
            TestMockupViewModel testMockupViewModel = new TestMockupViewModel
            {
                LstTestTechnology = Task.Run(async () => await LookUpDomainValuesDomainLogic.GetLookUpDomainValueByLookUpCode(LookUpDomainCode.Technology)).Result,

            };
            ModelState.Clear();

            return PartialView(testMockupViewModel);
        }

        [ChildActionOnly]
        public ActionResult _TestDuration()
        {
            return PartialView();
        }


        [ChildActionOnly]
        public ActionResult _PreviewTest()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _SampleTest()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _SendInvitation()
        {
            return PartialView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SampleTestId"></param>
        /// <returns></returns>
        public ActionResult _GetSampleTests(List<SampleTestMockups> sampleTestlst)
        {
            return PartialView(sampleTestlst);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedTechnologies"></param>
        /// <param name="experience"></param>
        /// <param name="duration"></param>
        /// <param name="isNagativeMarking"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> PrepareTest(Guid[] selectedTechnologies, Guid experience, int duration, bool isNagativeMarking)
        {
            if (!Request.IsAjaxRequest()) { return null; }
            InviteTestDomainLogic inviteTestDomainLogic = new InviteTestDomainLogic();
            return Json(new
            {
                success = true,
                data = await inviteTestDomainLogic.GenerateSampleTest(selectedTechnologies, duration, experience, isNagativeMarking)
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTestId"></param>
        /// <returns></returns>

        public async Task<ActionResult> PreviewTestById(Guid sampleTestId)
        {
            ViewBag.ModelTitle = "Questions";
            InviteTestDomainLogic inviteTestDomainLogic = new InviteTestDomainLogic();
            return View(await inviteTestDomainLogic.GetSampleTestById(sampleTestId));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTestId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> PrepareTestPaper(Guid sampleTestId)
        {
            if (!Request.IsAjaxRequest()) { return null; }
            InviteTestDomainLogic inviteTestDomainLogic = new InviteTestDomainLogic();
            TestPapers testPapers = await inviteTestDomainLogic.PrepareTestPaper(sampleTestId);

            return Json(new
            {
                data = Json_AllowGet_IgnoreReferenceLoop(testPapers),
                success = true
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendTestInvitationViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SendTestInvitation(TestInvitationViewModel sendTestInvitationViewModel)
        {
            string emails = Utilities.Validations.IsValidMultipleEmail(sendTestInvitationViewModel.EmailFromEmailAddress);
            if (emails != string.Empty)
            {
                ModelState.AddModelError("InValidEmail", "Following email address are wrong : " + emails);
            }

            if (ModelState.IsValid)
            {
                InviteTestDomainLogic inviteTestDomainLogic = new InviteTestDomainLogic();
                await inviteTestDomainLogic.SendTestInvitation(sendTestInvitationViewModel);
                return ReturnAjaxSuccessMessage("Invitaion sent  successfully", "/inviteTest/index");
            }
            return ReturnAjaxModelError();
        }
    }
}