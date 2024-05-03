using OnlineTestApp.DataAccess.ManageCompany;
using System;
using System.Collections.Generic;
using OnlineTestApp.ViewModel.Company;
using OnlineTestApp.Domain.Company;
namespace OnlineTestAppDomainLogic.Admin.ManageCompany
{
    public class ManageCompanyDomainLogic : OnlineTestApp.DomainLogic.Admin.BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ViewCompanyViewModel ViewCompanies(ViewCompanyViewModel viewCompanyViewModel)
        {
            switch (viewCompanyViewModel.SortBy)
            {
                case "CompanyName asc":
                case "CompanyName desc":
                case "CreatedDateTime asc":
                case "CreatedDateTime desc":
                    break;

                default:
                    viewCompanyViewModel.SortBy = "CreatedDateTime desc";
                    break;
            }
            viewCompanyViewModel.QueryString = GetQueryStringsForSorting();
            using (ManageCompanyDataAccess obj = new ManageCompanyDataAccess())
            {
                return obj.ViewCompanies(viewCompanyViewModel);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public Companies GetCompanyDetailsById(Guid companyId)
        {
            using (ManageCompanyDataAccess obj = new ManageCompanyDataAccess())
            {
                return obj.GetCompanyDetailsById(companyId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        public bool UpdateCompanyDetails(Companies company)
        {
            using (ManageCompanyDataAccess obj = new ManageCompanyDataAccess())
            {
                return obj.UpdateCompanyDetails(company);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        public bool AddNewCompany(Companies company)
        {
            using (ManageCompanyDataAccess obj = new ManageCompanyDataAccess())
            {
                return obj.AddNewCompany(company);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        public void DeleteCompany(Guid companyId)
        {
            using (ManageCompanyDataAccess obj = new ManageCompanyDataAccess())
            {
                obj.DeleteCompany(companyId);
            }
        }
    }
}
