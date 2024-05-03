using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
using OnlineTestApp.Domain.User;

namespace OnlineTestApp.Domain.Email
{
    public class EmailLog : BaseClasses.DomainBase
    {
        /// <summary>
        /// 
        /// </summary>
        public EmailLog()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendEmail"></param>
        public EmailLog(SendEmail sendEmail)
        {
            EmailLogId = Guid.NewGuid();
            EmailToEmailAddress = sendEmail.EmailToEmailAddress;
            EmailToName = sendEmail.EmailToName;
            FkEmailTemplateId = sendEmail.EmailTemplateId;
            EmailTemplateName = sendEmail.EmailTemplateName;
            EmailSubject = sendEmail.EmailSubject;
            EmailFromName = sendEmail.EmailFromName;
            EmailFromEmailAddress = sendEmail.EmailFromEmailAddress;
            ReplyToName = sendEmail.ReplyToName;
            ReplyToEmailAddress = sendEmail.ReplyToEmailAddress;
            EmailBody = sendEmail.EmailBody;
            EmailSendToCandidateId = sendEmail.EmailSendToCandidateId;
            EmailSendToApplicationUserId = sendEmail.EmailSendToApplicationUserId;
            FkCreatedBy = sendEmail.EmailSentBy;
        }
        [Key]
        public Guid EmailLogId { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid email address")]
        [Column(TypeName = "varchar")]
        [StringLength(256)]
        public string EmailToEmailAddress { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(256)]
        public string EmailToName { get; set; }

        [ForeignKey("EmailTemplate")]
        public Guid? FkEmailTemplateId { get; set; }

        public virtual EmailTemplates EmailTemplate { get; set; }

        //[Required(ErrorMessage = "Please enter template name")]
        [StringLength(500, ErrorMessage = "Email template name cannot be longer than 500 characters")]
        [Column(TypeName = "varchar")]
        public string EmailTemplateName { get; set; }

        [Required(ErrorMessage = "Please enter subject")]
        [StringLength(2000, ErrorMessage = "Email subject cannot be longer than 2000 characters")]
        [Column(TypeName = "varchar")]
        public string EmailSubject { get; set; }

        [StringLength(256, ErrorMessage = "Email from name cannot be longer than 100 characters")]
        [Column(TypeName = "varchar")]
        public string EmailFromName { get; set; }

        [StringLength(100, ErrorMessage = "Email from emailaddress cannot be longer than 100 characters")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid email address")]
        [Column(TypeName = "varchar")]
        public string EmailFromEmailAddress { get; set; }

        [StringLength(100, ErrorMessage = "Email from name cannot be longer than 100 characters")]
        [Column(TypeName = "varchar")]
        public string ReplyToName { get; set; }

        [StringLength(100, ErrorMessage = "Email from emailaddress cannot be longer than 100 characters")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid email address")]
        [Column(TypeName = "varchar")]
        public string ReplyToEmailAddress { get; set; }


        [Required(ErrorMessage = "Please enter email body")]
        [Column(TypeName = "varchar")]
        [StringLength(256)]
        public string EmailBodyFileName { get; set; }

        [NotMapped]
        public string EmailBody { get; set; }

        [ForeignKey("Candidate")]
        public Guid? EmailSendToCandidateId { get; set; }

        public virtual Candidate.Candidates Candidate { get; set; }

        [ForeignKey("ApplicationUsers")]
        public Guid? EmailSendToApplicationUserId { get; set; }

        public virtual ApplicationUsers ApplicationUsers { get; set; }

        public bool IsEmailSent { get; set; }

        public string EmailNotSentError { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid FkCreatedBy { get; set; }

        public virtual ApplicationUsers CreatedByUser { get; set; }
    }
}
