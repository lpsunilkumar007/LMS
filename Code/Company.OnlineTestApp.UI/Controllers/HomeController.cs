using Company.OnlineTestApp.UI.Controllers.Base;
using OnlineTestApp.DomainLogic.Admin.Common;
using OnlineTestApp.Enums.LookUps;
using OnlineTestApp.ViewModel.Company;
using OnlineTestApp.ViewModel.Test;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Company.OnlineTestApp.UI.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            TestMockupViewModel  sampleTestMockupViewModel = new TestMockupViewModel
            {
                LstExperienceLevel = await LookUpDomainValuesDomainLogic.GetLookUpDomainValueByLookUpCode(LookUpDomainCode.QuestionLevels),
                LstTestTechnology = await LookUpDomainValuesDomainLogic.GetLookUpDomainValueByLookUpCode(LookUpDomainCode.Technology)
                
            };
            ModelState.Clear();
            return View(sampleTestMockupViewModel);
        }

    }
}