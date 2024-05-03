using OnlineTestApp.Domain.Company;
using OnlineTestApp.ViewModel.Company;
using OnlineTestAppDomainLogic.Admin.ManageCompany;
using System;
using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.Company
{
    public class CompaniesController : BaseClasses.SuperAdminUserControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddNewCompany()
        {
            return View(new Companies());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companies"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult AddNewCompany(Companies companies)
        {
            if (ModelState.IsValid)
            {
                ManageCompanyDomainLogic obj = new ManageCompanyDomainLogic();
                if (obj.AddNewCompany(companies))
                {
                    return ReturnAjaxSuccessMessage("Company added successfully");
                }
                return ReturnAjaxErrorMessage("Company name already exists. Please try again");

            }
            return ReturnAjaxModelError();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewCompanies(ViewCompanyViewModel viewCompanyViewModel)
        {
            ManageCompanyDomainLogic obj = new ManageCompanyDomainLogic();
            return View(obj.ViewCompanies(viewCompanyViewModel));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCompanyDetails(Guid companyId)
        {
            ManageCompanyDomainLogic obj = new ManageCompanyDomainLogic();
            return View(obj.GetCompanyDetailsById(companyId));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companies"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult UpdateCompanyDetails(Companies companies, Guid companyId)
        {
            companies.CompanyId = companyId;
            if (ModelState.IsValid)
            {
                ManageCompanyDomainLogic obj = new ManageCompanyDomainLogic();
                if (obj.UpdateCompanyDetails(companies))
                {
                    return ReturnAjaxSuccessMessage("Company details updated successfully");
                }
                return ReturnAjaxErrorMessage("Company name already exists. Please try again");

            }
            return ReturnAjaxModelError();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        [HttpPost]
        public void DeleteCompany(Guid companyId)
        {
            if (!Request.IsAjaxRequest()) return;
            ManageCompanyDomainLogic obj = new ManageCompanyDomainLogic();
            obj.DeleteCompany(companyId);
        }


    }
}