using OnlineTestApp.Domain.User;
using System.Linq;
using System.Data.Entity;
using OnlineTestApp.Enums.User;
using System.Threading.Tasks;
using OnlineTestApp.ViewModel.UserMemberShip;

namespace OnlineTestApp.DataAccess.Company
{
    public class CompanyUserDataAccess : BaseClasses.DataAccessBase
    {
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApplicationUsers CheckValidUserByEmail(string email)
        {
            return _DbContext.ApplicationUsers
               .Include(x => x.ApplicationUserRoles)
               .Include(x => x.UserCompany)
               .Where(x => x.EmailAddress == email && x.IsActive == true)
               .SingleOrDefault();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public ApplicationUsers UserLogin(CompanyUserViewModel userLogin)
        {
            return _DbContext.ApplicationUsers
                .Include(x => x.ApplicationUserRoles)
                .Include(x => x.UserCompany)
                .Where(x => x.EmailAddress == userLogin.EmailAddress && x.UserPassword == userLogin.UserPassword && x.FkUserRoleId == (short)UserRoles.CompanyAdmin)
                .SingleOrDefault();
            ;
        }
    }
}
