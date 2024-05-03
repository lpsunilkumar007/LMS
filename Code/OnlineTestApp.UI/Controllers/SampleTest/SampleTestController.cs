using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.SampleTest
{
    public class SampleTestController : BaseClasses.AnonymousUserControllerBase
    {
        // GET: SampleTestMockup
        public async Task<ActionResult> Index()
        {
            DomainLogic.Admin.SampleTest.SampleTestDomainLogic sampleTestDomainLogic = new DomainLogic.Admin.SampleTest.SampleTestDomainLogic();
            await sampleTestDomainLogic.GenerateSampleTest();           
            return View();
        }
        public async Task<ActionResult> SamplePaper(Guid samplePaperId)
        {
            DomainLogic.Admin.SampleTest.SampleTestDomainLogic sampleTestDomainLogic = new DomainLogic.Admin.SampleTest.SampleTestDomainLogic();
            return View(await sampleTestDomainLogic.GetSampleTestById(samplePaperId));          
        }

    }
}