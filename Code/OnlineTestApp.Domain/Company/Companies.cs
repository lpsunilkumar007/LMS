using OnlineTestApp.Domain.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestApp.Domain.Company
{
    public class Companies : BaseClasses.DomainBase
    {
        public Companies()
        {
            CompanyId = Guid.NewGuid();
            CanDisplayCompany = true;
            if (UserVariables.IsAuthenticated)
            {
                FkCreatedBy = UserVariables.LoggedInUserId;
            }
        }

        [Key]
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = "Please enter company name")]
        [StringLength(256, ErrorMessage = "Company name cannot be longer than 256 characters")]
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        //public string test { get; set; }

        public virtual ICollection<ApplicationUsers> LstUsers { get; set; }

        public bool CanDisplayCompany { get; set; }

        public Guid? FkCreatedBy { get; set; }

        [StringLength(50, ErrorMessage = "Org No cannot be longer than 50 characters")]
        [Required(ErrorMessage = "Please enter Org No")]
        [Display(Name = "Org no")]
        public string OrgNo { get; set; }

        [StringLength(50, ErrorMessage = "Telephone number cannot be longer than 50 characters")]
        [Required(ErrorMessage = "Please enter telephone number")]
        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [StringLength(256, ErrorMessage = "Email address cannot be longer than 256 characters")]
        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid email address")]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        #region 
        /// <summary>
        /// 
        /// </summary>
        bool isActive = true, isDeleted = false;

        [Required]
        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }
        /// <summary>
        /// 
        /// </summary>

        [Required]
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// 
        /// </summary>

        public string IsActive_YesNo
        {
            get
            {
                return isActive ? "Yes" : "No";
            }
        }

        public DateTime? DeletedDateDateTime
        {
            get; set;
        }


        public Guid? FkDeletedBy { get; set; }
        #endregion


    }
}
