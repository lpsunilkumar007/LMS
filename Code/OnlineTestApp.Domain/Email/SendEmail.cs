using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OnlineTestApp.Domain.Email
{
    public class SendEmail : BaseClasses.DomainBase
    {
        public SendEmail()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailTemplates"></param>
        public SendEmail(EmailTemplates emailTemplates)
        {
            if (emailTemplates == null)
            {
                emailTemplates = new EmailTemplates();
            }
            else
            {
                EmailTemplateId = emailTemplates.EmailTemplateId;
            }

            EmailTemplateName = emailTemplates.EmailTemplateName;
            EmailSubject = emailTemplates.EmailSubject;
            EmailFromName = emailTemplates.EmailFromName;
            EmailFromEmailAddress = emailTemplates.EmailFromEmailAddress;
            ReplyToName = emailTemplates.ReplyToName;
            ReplyToEmailAddress = emailTemplates.ReplyToEmailAddress;
            EmailTemplateDescription = emailTemplates.EmailTemplateDescription;
            EmailBody = emailTemplates.EmailBody;
        }

        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid email address")]
        //[StringLength(256)]

        [Required(ErrorMessage = "Please enter email to")]
        public string EmailToEmailAddress { get; set; }

        [StringLength(256)]
        public string EmailToName { get; set; }


        public Guid? EmailTemplateId { get; set; }

        //[Required(ErrorMessage = "Please enter template name")]
        [StringLength(500, ErrorMessage = "Email template name cannot be longer than 500 characters")]
        public string EmailTemplateName { get; set; }

        [Required(ErrorMessage = "Please enter subject")]
        [StringLength(2000, ErrorMessage = "Email subject cannot be longer than 2000 characters")]
        public string EmailSubject { get; set; }

        [StringLength(256, ErrorMessage = "Email from name cannot be longer than 100 characters")]
        public string EmailFromName { get; set; }

        [StringLength(100, ErrorMessage = "Email from emailaddress cannot be longer than 100 characters")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid email address")]
        public string EmailFromEmailAddress { get; set; }

        [StringLength(100, ErrorMessage = "Email from name cannot be longer than 100 characters")]
        public string ReplyToName { get; set; }

        [StringLength(100, ErrorMessage = "Email from emailaddress cannot be longer than 100 characters")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid email address")]
        public string ReplyToEmailAddress { get; set; }

        [StringLength(3000, ErrorMessage = "Email description cannot be longer than 3000 characters")]
        public string EmailTemplateDescription { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Please enter email body")]
        public string EmailBody { get; set; }

        public Guid EmailSentBy { get; set; }

        public Guid? EmailSendToCandidateId { get; set; }

        public Guid? EmailSendToApplicationUserId { get; set; }
    }
}
