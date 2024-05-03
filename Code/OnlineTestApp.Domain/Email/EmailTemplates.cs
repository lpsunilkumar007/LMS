using OnlineTestApp.Domain.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OnlineTestApp.Domain.Email
{
    public class EmailTemplates : BaseClasses.DomainBase
    {
        public EmailTemplates()
        {
            EmailTemplateId = Guid.NewGuid();
        }
        [Key]
        public Guid EmailTemplateId { get; set; }

        [Required(ErrorMessage = "Please enter template name")]
        [StringLength(500, ErrorMessage = "Email template name cannot be longer than 500 characters")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Email template name")]
        public string EmailTemplateName { get; set; }

        [Required(ErrorMessage = "Please enter subject")]
        [StringLength(2000, ErrorMessage = "Email subject cannot be longer than 2000 characters")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Email subject")]
        public string EmailSubject { get; set; }

        [StringLength(256, ErrorMessage = "Email from name cannot be longer than 100 characters")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Email from name")]
        public string EmailFromName { get; set; }

        [StringLength(100, ErrorMessage = "Email from emailaddress cannot be longer than 100 characters")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid email address")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Email from email address")]
        public string EmailFromEmailAddress { get; set; }

        [StringLength(100, ErrorMessage = "Email from name cannot be longer than 100 characters")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Reply to name")]
        public string ReplyToName { get; set; }

        [StringLength(100, ErrorMessage = "Email from emailaddress cannot be longer than 100 characters")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid email address")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Reply to email address")]
        public string ReplyToEmailAddress { get; set; }

        [StringLength(3000, ErrorMessage = "Email description cannot be longer than 3000 characters")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Email template description")]
        public string EmailTemplateDescription { get; set; }

        [AllowHtml]
        [Display(Name = "Email body")]
        [Required(ErrorMessage = "Please enter email body")]
        public string EmailBody { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid FkCreatedBy { get; set; }
        public virtual ApplicationUsers CreatedByUser { get; set; }

        [ForeignKey("Company")]
        public Guid? FkAssignedCompanyId { get; set; }
        public virtual Company.Companies Company { get; set; }


        public Enums.Email.EmailTemplateCode? EmailTemplateCode { get; set; }

        public bool IsSystemTemplate { get; set; }

        public bool IsSharedTemplate { get; set; }

        #region 
        /// <summary>
        /// 
        /// </summary>
        bool isDeleted = false;

        [Required]
        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }




        public DateTime? DeletedDateDateTime
        {
            get; set;
        }


        public Guid? FkDeletedBy { get; set; }
        #endregion

    }
}
