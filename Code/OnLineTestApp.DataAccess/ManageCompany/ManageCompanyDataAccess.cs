using System;
using System.Linq.Dynamic;
using System.Data.Entity;
using System.Linq;

namespace OnlineTestApp.DataAccess.ManageCompany
{
    public class ManageCompanyDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ViewModel.Company.ViewCompanyViewModel ViewCompanies(ViewModel.Company.ViewCompanyViewModel viewCompanyViewModel)
        {
            var query = _DbContext.Company.Where(x => x.IsDeleted == false && x.CanDisplayCompany == true);

            if (!string.IsNullOrEmpty(viewCompanyViewModel.FreeTextBox))
            {
                query = query.Where(
                    x => x.CompanyName.Contains(viewCompanyViewModel.FreeTextBox) ||
                    x.OrgNo.Contains(viewCompanyViewModel.FreeTextBox) ||
                    x.Telephone.Contains(viewCompanyViewModel.FreeTextBox) ||
                    x.EmailAddress.Contains(viewCompanyViewModel.FreeTextBox)
                    );
            }


            viewCompanyViewModel.LstCompanies = query.OrderBy(viewCompanyViewModel.SortBy)
                .Skip(viewCompanyViewModel.SkipRecords)
                .Take(viewCompanyViewModel.PageSize).ToList();
            viewCompanyViewModel.TotalRecords = query.Count();

            return viewCompanyViewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public Domain.Company.Companies GetCompanyDetailsById(Guid companyId)
        {
            return _DbContext.Company.Where(x => x.CompanyId == companyId
            && x.IsDeleted == false).Single();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        public bool UpdateCompanyDetails(Domain.Company.Companies company)
        {
            var exists = _DbContext.Company.Where(x => x.CompanyName == company.CompanyName && x.CompanyId != company.CompanyId).Any();
            if (exists) return false;
            var orginalData = GetCompanyDetailsById(company.CompanyId);
            orginalData.CompanyName = company.CompanyName;
            orginalData.OrgNo = company.OrgNo;
            orginalData.Telephone = company.Telephone;
            orginalData.EmailAddress = company.EmailAddress;
            _DbContext.Entry(orginalData).State = EntityState.Modified;
            _DbContext.SaveChanges(createLog: true);

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        public bool AddNewCompany(Domain.Company.Companies company)
        {
            var exists = _DbContext.Company.Where(x => x.CompanyName == company.CompanyName).Any();
            if (exists) return false;
            _DbContext.Company.Add(company);
            _DbContext.SaveChanges(createLog: false);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        public void DeleteCompany(Guid companyId)
        {
            Guid loggedInUserId = UserVariables.LoggedInUserId;
            var originalData = _DbContext.Company.Where(x => x.CompanyId == companyId && x.IsDeleted == false).Single();
            originalData.IsDeleted = true;
            originalData.DeletedDateDateTime = DateSettings.CurrentDateTime;
            originalData.FkDeletedBy = loggedInUserId;
            _DbContext.Entry(originalData).State = EntityState.Modified;
            _DbContext.SaveChanges(createLog: false);
        }
    }
}
