using System.Linq;
using OnlineTestApp.Domain.User;
using System.Data.Entity;
namespace OnlineTestApp.DataAccess.UserMemberShip
{
    public class UserLoginDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        public ApplicationUsers UserLogin(ViewModel.UserMemberShip.UserLoginViewModel userLogin)
        {
            return _DbContext.ApplicationUsers
                .Include(x => x.ApplicationUserRoles)
                .Include(x => x.UserCompany)
                .Where(x => x.UserName == userLogin.UserName && x.UserPassword == userLogin.UserPassword)
                .SingleOrDefault();
            ;
        }
    }
}
