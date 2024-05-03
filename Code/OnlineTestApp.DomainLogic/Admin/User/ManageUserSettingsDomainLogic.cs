using OnlineTestApp.DataAccess.User;
using System;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.User
{
    public class ManageUserSettingsDomainLogic : BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUserId"></param>
        /// <returns></returns>
        public async Task AssignDefaultSettingToUser(Guid applicationUserId)
        {
            using (ManageUserSettingsDataAccess obj = new ManageUserSettingsDataAccess())
            {
                await obj.AssignDefaultSettingToUser(applicationUserId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task ChangeLeftMenuSetting()
        {
            using (ManageUserSettingsDataAccess obj = new ManageUserSettingsDataAccess())
            {
                await obj.ChangeLeftMenuSetting(UserVariables.LoggedInUserId);
            }
        }
    }
}
