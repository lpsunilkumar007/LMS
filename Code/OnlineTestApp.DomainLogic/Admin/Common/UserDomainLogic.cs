using OnlineTestApp.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.Common
{
    public static class UserDomainLogic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Domain.User.ApplicationUserRoles>> GetApplicationUserRoles()
        {
            using (UserDataAccess obj = new UserDataAccess())
            {
                return await obj.GetApplicationUserRoles();
            }
        }
        /// <summary>
        /// Get logged in user details
        /// </summary>
        /// <returns></returns>
        public static async Task<Domain.User.ApplicationUsers> GetUserDetailsAsync()
        {
            return await GetUserDetailsAsync(UserVariables.LoggedInUserId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggedInUserId"></param>
        /// <returns></returns>
        public static async Task<Domain.User.ApplicationUsers> GetUserDetailsAsync(Guid loggedInUserId)
        {
            using (UserDataAccess obj = new UserDataAccess())
            {
                return await obj.GetUserDetailsAsync(loggedInUserId);
            }
        }

       
    }
}
