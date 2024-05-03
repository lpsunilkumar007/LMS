using System;
using System.Web;
namespace OnlineTestApp
{
    public class UserVariables
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        static object GetValue(int index, char split = '}')
        {
            string userData = HttpContext.Current.User.Identity.Name;
            string[] st = userData.Split(split);
            if (st.Length <= 0) throw new InvalidOperationException("Out of index");
            return st[index];
        }
        /// <summary>
        /// Login User ID -- 0
        /// </summary>
        public static Guid LoggedInUserId
        {
            get
            {
                return new Guid(GetValue(0).ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static bool CanAccessEverything
        {
            get
            {
                return IsSuperAdmin || IsSystemUser;
            }
        }
        /// <summary>
        /// Is LoggedIn User Super Admin -- 1
        /// </summary>
        public static bool IsSuperAdmin
        {
            get
            {
                return GetValue(1).ToType<bool>();
            }
        }
        /// <summary>
        /// Is LoggedIn User Systsem User -- 1
        /// </summary>
        public static bool IsSystemUser
        {
            get
            {
                return GetValue(2).ToType<bool>();
            }
        }
        #region Admin Roles
        /// <summary>
        /// 
        /// </summary>
        public static bool CanAccessAdminModules
        {
            get
            {
                return IsCompanyAdmin || IsAdmin;
            }
        }
        public static bool IsCompanyAdmin
        {
            get
            {
                return UserRole == Enums.User.UserRoles.CompanyAdmin;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static bool IsAdmin
        {
            get
            {
                return UserRole == Enums.User.UserRoles.Admin;
            }
        }
        #endregion
        /// <summary>
        /// LoggedIn User CompanyId Id -- 3
        /// </summary>
        public static Guid UserCompanyId
        {
            get
            {
                return new Guid(GetValue(3).ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static Enums.User.UserRoles UserRole
        {
            get
            {
                return GetValue(4).ToEnum<Enums.User.UserRoles>();
            }
        }
        /// <summary>
        /// Check current user is login or not
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                if (HttpContext.Current == null) return false;
                if (HttpContext.Current.User == null) return false;
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        //public static Guid LoggedInUserId
        //{
        //    get
        //    {
        //        return new Guid(System.Web.HttpContext.Current.User.Identity.Name);
        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public static bool IsAuthenticated
        //{
        //    get
        //    {
        //        return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userRole"></param>
        ///// <returns></returns>
        //public static bool UserIsInRole(string userRole)
        //{
        //    return System.Web.HttpContext.Current.User.IsInRole(userRole);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="userRoles"></param>
        //public static IPrincipal CreateCookie(Guid userId, List<string> userRoles)
        //{
        //    return new GenericPrincipal(
        //           new GenericIdentity(userId.ToString()), userRoles.ToArray());
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="userRole"></param>
        //public static IPrincipal CreateCookie(Guid userId, string userRoleIds)
        //{
        //    List<string> userRoles = new List<string>();
        //    userRoles.Add(userRoleIds);
        //    return CreateCookie(userId, userRoles);
        //}
    }
}
