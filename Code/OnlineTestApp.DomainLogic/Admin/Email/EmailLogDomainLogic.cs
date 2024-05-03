using OnlineTestApp.DataAccess.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.Email
{
    public class EmailLogDomainLogic : OnlineTestApp.DomainLogic.Admin.BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailLog"></param>
        /// <returns></returns>
        public async Task CreateEmailLog(Domain.Email.EmailLog emailLog)
        {
            using (EmailLogDataAccess obj = new EmailLogDataAccess())
            {
                string emailBodyFileName = Guid.NewGuid().ToString() + ".html";
                if (!string.IsNullOrEmpty(emailLog.EmailBody))
                {
                    string emailBodyFilePath = "";
                    if (emailLog.EmailSendToCandidateId.HasValue)
                    {
                        emailBodyFilePath = Settings.FileSystemDomainLogic.GetCandidateEmailLogBodyPath(emailLog.EmailSendToCandidateId.Value);
                    }
                    //else if (emailLog.EmailSendToApplicationUserId.HasValue)
                    //{
                    //    emailBodyFilePath = Settings.FileSystemDomainLogic.GetApplicationUserEmailLogBodyPath(emailLog.EmailSendToApplicationUserId.Value);
                    //}

                    emailBodyFilePath = emailBodyFilePath + emailBodyFileName;

                    Utilities.FileSystem.CreateFile(emailLog.EmailBody, emailBodyFilePath);

                }
                emailLog.EmailBodyFileName = emailBodyFileName;
                await obj.CreateEmailLog(emailLog);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Domain.Email.EmailLog> GetUserEmailLog()
        {
            using (EmailLogDataAccess obj = new EmailLogDataAccess())
            {
                return obj.GetUserEmailLog(UserVariables.LoggedInUserId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailLogId"></param>
        /// <returns></returns>
        public Domain.Email.EmailLog GetEmailLogDetail(Guid emailLogId)
        {
            using (EmailLogDataAccess obj = new EmailLogDataAccess())
            {
                var result = obj.GetEmailLogDetail(emailLogId);

                string filePath = "";
                if (!string.IsNullOrEmpty(result.EmailBodyFileName))
                {
                    if (result.EmailSendToCandidateId.HasValue)
                    {
                        filePath = Settings.FileSystemDomainLogic.
                            GetCandidateEmailLogBodyPath(result.EmailSendToCandidateId.Value);
                    }
                    //else if (result.EmailSendToApplicationUserId.HasValue)
                    //{
                    //    filePath = Settings.FileSystemDomainLogic.
                    //        GetApplicationUserEmailLogBodyPath(result.EmailSendToApplicationUserId.Value);
                    //}
                }


                if (!string.IsNullOrEmpty(filePath))
                {
                    filePath = filePath + "/" + result.EmailBodyFileName;

                    string newFileName = Guid.NewGuid() + ".html";
                    Utilities.FileSystem.Copy(filePath, Settings.FileSystemDomainLogic.TempFolderPath + newFileName);

                    result.EmailBodyFileName = newFileName;
                }

                return result;
            }
        }
    }
}
