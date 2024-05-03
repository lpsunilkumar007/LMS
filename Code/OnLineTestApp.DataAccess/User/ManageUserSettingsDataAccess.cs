using OnlineTestApp.Domain.User;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.DataAccess.User
{
    public class ManageUserSettingsDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUserId"></param>
        /// <returns></returns>
        public async Task AssignDefaultSettingToUser(Guid applicationUserId)
        {
            var exists = await _DbContext.ApplicationUserSettings.Where(x => x.ApplicationUserId == applicationUserId).SingleOrDefaultAsync();
            if (exists == null)
            {
                exists = new ApplicationUserSettings { ApplicationUserId = applicationUserId };
                _DbContext.ApplicationUserSettings.Add(exists);
                await _DbContext.SaveChangesAsync(createLog: false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUserId"></param>
        /// <returns></returns>
        public async Task ChangeLeftMenuSetting(Guid applicationUserId)
        {
            var originalRecord = await _DbContext.ApplicationUserSettings.Where(x => x.ApplicationUserId == applicationUserId).SingleOrDefaultAsync();

            originalRecord.IsMenuOpen = originalRecord.IsMenuOpen ? false : true;
            _DbContext.Entry(originalRecord).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync(createLog: true);


        }
    }
}
