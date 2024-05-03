using OnlineTestApp.Domain.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.DataAccess.Email
{
    public class EmailLogDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailLog"></param>
        /// <returns></returns>
        public async Task CreateEmailLog(EmailLog emailLog)
        {
            _DbContext.EmailLog.Add(emailLog);
            await _DbContext.SaveChangesAsync(createLog: false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggedInUserId"></param>
        /// <returns></returns>
        public List<EmailLog> GetUserEmailLog(Guid loggedInUserId)
        {
            return _DbContext.EmailLog.Where(x => x.FkCreatedBy == loggedInUserId)
                .OrderByDescending(x => x.CreatedDateTime).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailLogId"></param>
        /// <returns></returns>
        public EmailLog GetEmailLogDetail(Guid emailLogId)
        {
            return _DbContext.EmailLog.Where(x => x.EmailLogId == emailLogId)
               .OrderByDescending(x => x.CreatedDateTime).Single();
        }
    }
}
