using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.DataAccess.Common
{
    public class UserDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Domain.User.ApplicationUserRoles>> GetApplicationUserRoles()
        {
            return await _DbContext.ApplicationUserRoles.Where(x => x.IsDeleted == false).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUserId"></param>
        /// <returns></returns>
        public async Task<Domain.User.ApplicationUsers> GetUserDetailsAsync(Guid applicationUserId)
        {
            return await _DbContext.ApplicationUsers.Where(x => x.IsDeleted == false && x.ApplicationUserId == applicationUserId).SingleAsync();
        }        
    }
}
