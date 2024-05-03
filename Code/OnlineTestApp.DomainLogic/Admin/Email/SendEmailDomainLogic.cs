using OnlineTestApp.DataAccess.Email;
using OnlineTestApp.Domain.Email;
using System;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.Email
{
    public class SendEmailDomainLogic : OnlineTestApp.DomainLogic.Admin.BaseClasses.DomainLogicBase
    {
        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendEmail"></param>
        /// <returns></returns>
        Utilities.EmailSender.Domain.EmailDomain GetEmailSenderDomain(SendEmail sendEmail)
        {
            return new Utilities.EmailSender.Domain.EmailDomain
            {
                EmailTo = sendEmail.EmailToEmailAddress,
                EmailToName = sendEmail.EmailToName,
                EmailCc = "",
                EmailCcName = "",
                EmailReply = sendEmail.ReplyToEmailAddress,
                EmailReplyName = sendEmail.ReplyToName,
                EmailSubject = sendEmail.EmailSubject,
                EmailBody = sendEmail.EmailBody,
                IsBodyHtml = true,
                EmailFrom = SystemSettings.EmailFromEmailAddress,
                EmailFromName = SystemSettings.EmailFromName,
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendEmail"></param>
        /// <returns></returns>
        async Task SendEmail(SendEmail sendEmail)
        {
            //replacing common 
            ReplaceCommonParameters(sendEmail);

            //replacing email sent by details
            await ReplaceEmailSentByData(sendEmail);

            var emailDomain = GetEmailSenderDomain(sendEmail);

            //creating log
            var emailLog = new EmailLog(sendEmail);

            if (string.IsNullOrEmpty(emailDomain.EmailTo)
                &&
                (emailDomain.ListOfMultpleEmailTo == null
               || emailDomain.ListOfMultpleEmailTo.Count == 0)
                )
            {
                emailLog.IsEmailSent = false;
                emailLog.EmailNotSentError = "No email address found";
            }
            else
            {
                await Utilities.EmailSender.EmailSender.SendEmail(emailDomain);
                emailLog.IsEmailSent = emailDomain.IsEmailSent;
                emailLog.EmailNotSentError = emailDomain.EmailNotSentError;
            }

            emailLog.EmailFromName = emailDomain.EmailFromName;
            emailLog.EmailFromEmailAddress = emailDomain.EmailFrom;

            EmailLogDomainLogic objEmailLog = new EmailLogDomainLogic();
            await objEmailLog.CreateEmailLog(emailLog);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendEmail"></param>
        /// <param name="loggedInUserId"></param>
        async Task ReplaceEmailSentByData(SendEmail sendEmail)
        {
            var userData = await Common.UserDomainLogic.GetUserDetailsAsync(sendEmail.EmailSentBy);
            sendEmail.EmailSubject = sendEmail.EmailSubject.Replace("@@ApplicationUser_UserName", userData.UserName);
            sendEmail.EmailSubject = sendEmail.EmailSubject.Replace("@@ApplicationUser_FirstName", userData.FirstName);
            sendEmail.EmailSubject = sendEmail.EmailSubject.Replace("@@ApplicationUser_LastName", userData.LastName);
            sendEmail.EmailSubject = sendEmail.EmailSubject.Replace("@@ApplicationUser_FullName", userData.FullName);
            sendEmail.EmailSubject = sendEmail.EmailSubject.Replace("@@ApplicationUser_EmailAddress", userData.EmailAddress);
            sendEmail.EmailSubject = sendEmail.EmailSubject.Replace("@@ApplicationUser_MobileNumber", userData.MobileNumber);
            sendEmail.EmailSubject = sendEmail.EmailSubject.Replace("@@ApplicationUser_AlternateNumber", userData.AlternateNumber);


            sendEmail.EmailBody = sendEmail.EmailBody.Replace("@@ApplicationUser_UserName", userData.UserName);
            sendEmail.EmailBody = sendEmail.EmailBody.Replace("@@ApplicationUser_FirstName", userData.FirstName);
            sendEmail.EmailBody = sendEmail.EmailBody.Replace("@@ApplicationUser_LastName", userData.LastName);
            sendEmail.EmailBody = sendEmail.EmailBody.Replace("@@ApplicationUser_FullName", userData.FullName);
            sendEmail.EmailBody = sendEmail.EmailBody.Replace("@@ApplicationUser_EmailAddress", userData.EmailAddress);
            sendEmail.EmailBody = sendEmail.EmailBody.Replace("@@ApplicationUser_MobileNumber", userData.MobileNumber);
            sendEmail.EmailBody = sendEmail.EmailBody.Replace("@@ApplicationUser_AlternateNumber", userData.AlternateNumber);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendEmail"></param>
        void ReplaceCommonParameters(SendEmail sendEmail)
        {           

            sendEmail.EmailBody = sendEmail.EmailBody.Replace("@@CopyRightYear", DateTime.Now.Year.ToString());
            sendEmail.EmailSubject = sendEmail.EmailSubject.Replace("@@CopyRightYear", DateTime.Now.Year.ToString());
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public async Task<Domain.Candidate.Candidates> GetCandidateData(Guid candidateId)
        {
            using (SendEmailDataAccess obj = new SendEmailDataAccess())
            {
                return await obj.GetCandidateData(candidateId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendEmail"></param>
        /// <returns></returns>
        public async Task EmailCandidate(SendEmail sendEmail)
        {
            await SendEmail(sendEmail);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fKCompanyId"></param>
        /// <param name="sbBody"></param>
        /// <param name="sSubject"></param>
        /// <returns></returns>
        public static async Task ReplaceCompanyData(Guid fKCompanyId, StringBuilder sbBody, StringBuilder sSubject)
        {
            var companyData = await Common.CompanyDomainLogic.GetCompanyDetailsById(fKCompanyId);
            sbBody.Replace("@@Company_Name", companyData.CompanyName);
            sbBody.Replace("@@Company_Org", companyData.OrgNo);
            sbBody.Replace("@@Company_Telephone", companyData.Telephone);
            sbBody.Replace("@@Company_EmailAddress", companyData.EmailAddress);

            sSubject.Replace("@@Company_Name", companyData.CompanyName);
            sSubject.Replace("@@Company_Org", companyData.OrgNo);
            sSubject.Replace("@@Company_Telephone", companyData.Telephone);
            sSubject.Replace("@@Company_EmailAddress", companyData.EmailAddress);
        }
    }
}
