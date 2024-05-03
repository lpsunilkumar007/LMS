using OnlineTestApp.DataAccess.User;
using OnlineTestApp.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.User
{
    public class ManageUsersDomainLogic : BaseClasses.DomainLogicBase
    {
        public List<Domain.User.ApplicationUsers> GetUsersDetails(ViewApplicationUserViewModel viewApplicationUserViewModel)
        {
            ManageUsersDataAccess obj = new ManageUsersDataAccess();
            List<Domain.User.ApplicationUsers> usersList = obj.getUsersDetails(viewApplicationUserViewModel);
            return usersList;
        }
        public async Task<ViewApplicationUserViewModel> ViewApplicationUsers(ViewApplicationUserViewModel viewApplicationUserViewModel)
        {
            switch (viewApplicationUserViewModel.SortBy)
            {
                case "UserName asc":
                case "UserName desc":

                case "UserCompany.CompanyName asc":
                case "UserCompany.CompanyName desc":

                case "CreatedDateTime asc":
                case "CreatedDateTime desc":
                    break;

                default:
                    viewApplicationUserViewModel.SortBy = "CreatedDateTime desc";
                    break;
            }
            viewApplicationUserViewModel.QueryString = GetQueryStringsForSorting();
            ManageUsersDataAccess obj = new ManageUsersDataAccess();

            viewApplicationUserViewModel.LstCompanies = await Common.CompanyDomainLogic.GetAllCompanies();
            return obj.ViewApplicationUsers(viewApplicationUserViewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addEditUserViewModel"></param>
        /// <returns></returns>
        public async Task<string> AddNewUser(AddEditApplicationUserViewModel addEditUserViewModel)
        {
            // addEditUserViewModel.ApplicationUsers.UserPassword = EncryptText(addEditUserViewModel.ApplicationUsers.UserPassword, addEditUserViewModel.ApplicationUsers.UserName);
            addEditUserViewModel.ApplicationUsers.UserPassword = EncryptText(addEditUserViewModel.ApplicationUsers.UserPassword, addEditUserViewModel.ApplicationUsers.EmailAddress);
            using (ManageUsersDataAccess obj = new ManageUsersDataAccess())
            {
                if (!obj.AddNewUser(addEditUserViewModel.ApplicationUsers))
                {
                   // return "Username already exists. Please try agian";
                    return "Email address already exists. Please try agian";
                }
            }
            ManageUserSettingsDomainLogic objSettings = new ManageUserSettingsDomainLogic();
            await objSettings.AssignDefaultSettingToUser(addEditUserViewModel.ApplicationUsers.ApplicationUserId);

            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationuserid"></param>
        /// <returns></returns>
        public AddEditApplicationUserViewModel GetUserDetailsById(Guid applicationuserid)
        {
            using (ManageUsersDataAccess obj = new ManageUsersDataAccess())
            {
                //getting details from database
                var result = obj.GetUserDetailsById(applicationuserid);

                //password dencrypt
                result.UserPassword = DecryptText(result.UserPassword, result.UserName);
                return new AddEditApplicationUserViewModel
                {
                    ApplicationUsers = result
                };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addEditUserViewModel"></param>
        /// <returns></returns>
        public string EditUserDetails(AddEditApplicationUserViewModel addEditUserViewModel)
        {
            addEditUserViewModel.ApplicationUsers.UserPassword = EncryptText(addEditUserViewModel.ApplicationUsers.UserPassword, addEditUserViewModel.ApplicationUsers.UserName);
            using (ManageUsersDataAccess obj = new ManageUsersDataAccess())
            {
                if (!obj.EditUserDetails(addEditUserViewModel.ApplicationUsers))
                {
                    return "Username already exists. Please try agian";
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUserId"></param>
        public void DeleteUser(Guid applicationUserId)
        {
            using (ManageUsersDataAccess obj = new ManageUsersDataAccess())
            {
                obj.DeleteUser(applicationUserId);
            }
        }

    }
}
