using OnlineTestApp.DataAccess.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.Menu
{
    public class MenuDomainLogic : BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// Get logged in user details
        /// </summary>
        /// <returns></returns>
        public static Domain.User.ApplicationUsers GetUserDetailsForTopMenu()
        {
            using (MenuDataAccess obj = new MenuDataAccess())
            {
                return obj.GetUserDetailsForTopMenu(UserVariables.LoggedInUserId);
            }
        }
    }
}
