using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.DataAccess.Menu
{
    public class MenuDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUserId"></param>
        /// <returns></returns>
        public Domain.User.ApplicationUsers GetUserDetailsForTopMenu(Guid applicationUserId)
        {
            return _DbContext.ApplicationUsers
                .Include(x=>x.ApplicationUserSettings)
                .Where(x => x.IsDeleted == false && x.ApplicationUserId == applicationUserId).Single();
        }
    }
}
