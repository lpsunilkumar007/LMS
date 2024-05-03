using OnlineTestApp.Domain.User;
using OnlineTestApp.ViewModel.MyAccount;
using System;
using System.Linq;

namespace OnlineTestApp.DataAccess.MyAccount
{
  public  class MyAccountDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ApplicationUsers GetEditProfileDetails(Guid loggedInUserId)
        {
            return _DbContext.ApplicationUsers.Where(x => x.ApplicationUserId == loggedInUserId).Single();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUser"></param>
        public void UpdateUserProfileDetails(ApplicationUsers applicationUser)
        {
            var originalData = _DbContext.ApplicationUsers.Where(x => x.ApplicationUserId == applicationUser.LoggedInUserId).Single();
            originalData.FirstName = applicationUser.FirstName;
            originalData.LastName = applicationUser.LastName;
            originalData.EmailAddress = applicationUser.EmailAddress;
            originalData.MobileNumber = applicationUser.MobileNumber;
            originalData.AlternateNumber = applicationUser.AlternateNumber;
            _DbContext.Entry(originalData).State = System.Data.Entity.EntityState.Modified;
            _DbContext.SaveChanges(createLog: true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        public bool ChangeUserPassword(ChangePasswordViewModel changePassword)
        {
            var originalData = _DbContext.ApplicationUsers.Where(x => x.ApplicationUserId == changePassword.LoggedInUserId).Single();
            //old password dose not match
            if (originalData.UserPassword != changePassword.OldPassword) return false;
            originalData.UserPassword = changePassword.NewPassword;
            _DbContext.Entry(originalData).State = System.Data.Entity.EntityState.Modified;
            _DbContext.SaveChanges(createLog: true);
            return true;
        }
    }
}
