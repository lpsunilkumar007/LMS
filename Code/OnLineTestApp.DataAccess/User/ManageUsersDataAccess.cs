using OnlineTestApp.Domain.User;
using OnlineTestApp.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace OnlineTestApp.DataAccess.User
{
    public class ManageUsersDataAccess : BaseClasses.DataAccessBase
    {
        public List<Domain.User.ApplicationUsers> getUsersDetails(ViewApplicationUserViewModel viewApplicationUserViewModel)
        {
            var query = _DbContext.ApplicationUsers
                .Include(x => x.UserCompany)
                .Include(x => x.ApplicationUserRoles)
                //.Include(x => x.UserCompany)
                .Where(x => x.IsDeleted == false && x.IsSystemUser == false && x.ApplicationUserId != UserVariables.LoggedInUserId);

            List<Domain.User.ApplicationUsers> userslist = query.OrderBy(x=>x.UserName)
                 .Skip(viewApplicationUserViewModel.SkipRecords)
                .Take(viewApplicationUserViewModel.PageSize).ToList();
            return userslist;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewApplicationUserViewModel"></param>
        /// <returns></returns>
        public ViewApplicationUserViewModel ViewApplicationUsers(ViewApplicationUserViewModel viewApplicationUserViewModel)
        {
            var query = _DbContext.ApplicationUsers
                .Include(x => x.UserCompany)
                .Include(x => x.ApplicationUserRoles)
                //.Include(x => x.UserCompany)
                .Where(x => x.IsDeleted == false && x.IsSystemUser == false && x.ApplicationUserId != UserVariables.LoggedInUserId);
            if (viewApplicationUserViewModel.CompanyId.HasValue)
            {
                query = query.Where(x => x.FkCompanyId == viewApplicationUserViewModel.CompanyId.Value);
            }

            if (viewApplicationUserViewModel.HasFreeText)
            {

                query = query.Where(x =>
                                        x.UserName.Contains(viewApplicationUserViewModel.FreeTextBox) ||
                                        x.FirstName.Contains(viewApplicationUserViewModel.FreeTextBox) ||
                                        x.LastName.Contains(viewApplicationUserViewModel.FreeTextBox) ||
                                        x.FullName.Contains(viewApplicationUserViewModel.FreeTextBox) ||
                                        x.EmailAddress.Contains(viewApplicationUserViewModel.FreeTextBox) ||
                                        x.MobileNumber.Contains(viewApplicationUserViewModel.FreeTextBox) ||
                                        x.AlternateNumber.Contains(viewApplicationUserViewModel.FreeTextBox)
                );
            }
            viewApplicationUserViewModel.LstApplicationUsers = query.OrderBy(viewApplicationUserViewModel.SortBy)
                 .Skip(viewApplicationUserViewModel.SkipRecords)
                .Take(viewApplicationUserViewModel.PageSize).ToList();

            viewApplicationUserViewModel.TotalRecords = query.Count();

            return viewApplicationUserViewModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        public bool AddNewUser(ApplicationUsers applicationUser)
        {
            bool isExists = _DbContext.ApplicationUsers.Where(x => x.EmailAddress == applicationUser.EmailAddress).Any();
            //email alreadyexists
            //usermame alreadyexists
            if (isExists) return false;

            _DbContext.ApplicationUsers.Add(applicationUser);
            _DbContext.SaveChanges(createLog: false);

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationuserid"></param>
        /// <returns></returns>
        public ApplicationUsers GetUserDetailsById(Guid applicationuserid)
        {
            return _DbContext.ApplicationUsers.Where(x => x.ApplicationUserId == applicationuserid && x.ApplicationUserId != UserVariables.LoggedInUserId && x.IsDeleted == false).Single();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        public bool EditUserDetails(ApplicationUsers applicationUser)
        {
            bool isExists = _DbContext.ApplicationUsers.Where(x => x.UserName == applicationUser.UserName &&
            x.ApplicationUserId != applicationUser.ApplicationUserId).Any();

            //usermame alreadyexists
            if (isExists) return false;

            var originalData = _DbContext.ApplicationUsers.Where(x => x.ApplicationUserId ==
            applicationUser.ApplicationUserId && x.IsDeleted == false).Single();

            originalData.UserName = applicationUser.UserName;
            originalData.UserPassword = applicationUser.UserPassword;
            originalData.FirstName = applicationUser.FirstName;
            originalData.LastName = applicationUser.LastName;
            originalData.EmailAddress = applicationUser.EmailAddress;
            originalData.IsActive = applicationUser.IsActive;

            originalData.MobileNumber = applicationUser.MobileNumber;
            originalData.AlternateNumber = applicationUser.AlternateNumber;

            originalData.FkCompanyId = applicationUser.FkCompanyId;
            originalData.FkUserRoleId = applicationUser.FkUserRoleId;

            _DbContext.Entry(originalData).State = EntityState.Modified;
            _DbContext.SaveChanges(createLog: true);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUserId"></param>
        /// <param name="loggedInUserId"></param>
        public void DeleteUser(Guid applicationUserId)
        {
            Guid loggedInUserId = UserVariables.LoggedInUserId;
            var originalData = _DbContext.ApplicationUsers.Where(x => x.ApplicationUserId ==
            applicationUserId && x.ApplicationUserId != loggedInUserId && x.IsDeleted == false).Single();
            originalData.IsDeleted = true;
            originalData.DeletedDateDateTime = DateSettings.CurrentDateTime;
            originalData.FkDeletedBy = loggedInUserId;
            _DbContext.Entry(originalData).State = EntityState.Modified;
            _DbContext.SaveChanges(createLog: false);
        }

    }
}
