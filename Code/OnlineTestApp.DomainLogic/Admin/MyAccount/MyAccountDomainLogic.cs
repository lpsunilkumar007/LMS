using OnlineTestApp.Domain.User;
using OnlineTestApp.ViewModel.MyAccount;
using System.Threading.Tasks;
using OnlineTestApp.DataAccess.MyAccount;

namespace OnlineTestApp.DomainLogic.Admin.MyAccount
{
    public class MyAccountDomainLogic : BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ApplicationUsers GetUserDetails()
        {
            using (MyAccountDataAccess obj = new MyAccountDataAccess())
            {
                return obj.GetEditProfileDetails(UserVariables.LoggedInUserId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUser"></param>
        public void UpdateUserProfileDetails(ApplicationUsers applicationUser)
        {
            using (MyAccountDataAccess obj = new MyAccountDataAccess())
            {
                obj.UpdateUserProfileDetails(applicationUser);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        public async Task<bool> ChangeUserPassword(ChangePasswordViewModel changePassword)
        {
            using (MyAccountDataAccess obj = new MyAccountDataAccess())
            {
                var userDetails = await Common.UserDomainLogic.GetUserDetailsAsync();
                changePassword.OldPassword = EncryptText(changePassword.OldPassword, userDetails.UserName);
                changePassword.NewPassword = EncryptText(changePassword.NewPassword, userDetails.UserName);
                return obj.ChangeUserPassword(changePassword);
            }
        }
    }
}
