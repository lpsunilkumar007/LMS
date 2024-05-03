using Company.OnlineTestApp.UI.Controllers.Base;
using OnlineTestApp.DomainLogic.Admin.Common;
using OnlineTestApp.DomainLogic.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Company.OnlineTestApp.UI.Controllers
{
    public class DashboardController : AuthorizeController
    {        
        /// <summary>
        /// dashboard 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Result()
        {
            InviteTestDomainLogic inviteTestDomainLogic = new InviteTestDomainLogic();
            return View(await inviteTestDomainLogic.GetTestReports());
        }
        /// <summary>
        /// get all  TestInvitATIONS AND diplay on pop up under selected  paper 
        /// </summary>
        /// <param name="testPaperId"></param>
        /// <param name="retrievePapers"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetCandidatesResultbyTestpaperId(Guid testPaperId, int retrievePapers)
        {
            ViewBag.ModelTitle = "Test Result";
            if (!Request.IsAjaxRequest()) { return null; }
            InviteTestDomainLogic inviteTestDomainLogic = new InviteTestDomainLogic();
            return View(await inviteTestDomainLogic.GetCandidatesResultbyTestpaperId(testPaperId, retrievePapers));
        }

        /// <summary>
        /// show all questions and answers which candidate has given 
        /// </summary>
        /// <param name="testInvitationId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult InvitedTestCandidateTestResults(Guid testInvitationId)
        {
            InviteTestDomainLogic inviteTestDomainLogic = new InviteTestDomainLogic();
            return View(inviteTestDomainLogic.GetInvitedTestCandidateTestResults(testInvitationId));
        }      
    }
}