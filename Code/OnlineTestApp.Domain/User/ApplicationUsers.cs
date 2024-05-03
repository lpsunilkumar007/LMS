using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineTestApp.Domain.User
{
    public class ApplicationUsers : BaseClasses.DomainBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ApplicationUsers()
        {
            ApplicationUserId = Guid.NewGuid();
            UserRegistrationType = Enums.User.UserRegistrationType.Manual;
            if (UserVariables.IsAuthenticated)
            {
                FkCreatedBy = UserVariables.LoggedInUserId;
            }
        }

        [Key]
        public Guid ApplicationUserId { get; set; }

        public string _userName = "";

        [Required(ErrorMessage = "Please enter user name")]
      //  [Index("IX_Unique_ApplicationUsers_UserName", IsUnique = true)]
        [StringLength(256, ErrorMessage = "User name cannot be longer than 256 characters")]
        [Display(Name = "User name")]
        public string UserName
        {
            get
            {
                return _userName.ToFirstLetterCapitalize();
            }
            set
            {
                _userName = value;
            }
        }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(256, ErrorMessage = "Password cannot be longer than 256 characters")]
        [Display(Name = "User password")]
        public string UserPassword { get; set; }

        [StringLength(500)]
        [Column(TypeName = "varchar")]
        public string SocialMediaUniqueId { get; set; }

        string _firstName = "", _lastName = "";

        [StringLength(256, ErrorMessage = "First name cannot be longer than 256 characters")]
        [Required(ErrorMessage = "Please enter first name")]
        [Display(Name = "First name")]
        public string FirstName
        {
            get
            {
                return _firstName.ToFirstLetterCapitalize();
            }
            set
            {
                _firstName = value;
            }
        }

        [StringLength(256, ErrorMessage = "Last name cannot be longer than 256 characters")]
        [Required(ErrorMessage = "Please enter last name")]
        [Display(Name = "Last name")]
        public string LastName
        {
            get
            {
                return _lastName.ToFirstLetterCapitalize();
            }
            set
            {
                _lastName = value;
            }
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Full name")]
        public string FullName
        {

            get
            {
                return FirstName + " " + LastName;
            }
            private set { /* needed for EF */ }
        }

        [StringLength(256, ErrorMessage = "Email address cannot be longer than 256 characters")]
        [Index("IX_Unique_ApplicationUsers_Email", IsUnique = true)]
        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid email address")]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Column(TypeName = "varchar")]
        public string UserSocialMediaData { get; set; }

        [StringLength(50, ErrorMessage = "Mobile number cannot be longer than 50 characters")]
        [Required(ErrorMessage = "Please enter mobile number")]
        [Display(Name = "Mobile number")]
        public string MobileNumber { get; set; }

        [StringLength(100, ErrorMessage = "Alternate number cannot be longer than 100 characters")]
        [Display(Name = "Alternate number")]
        public string AlternateNumber { get; set; }

        [Required]
        public Enums.User.UserRegistrationType UserRegistrationType { get; set; }

        public bool IsSystemUser { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? FkCreatedBy { get; set; }

        public virtual ApplicationUsers CreatedByUser { get; set; }

        [Required(ErrorMessage = "Please select company")]
        [Display(Name = "Company")]
        public Guid FkCompanyId { get; set; }

        [ForeignKey("FkCompanyId")]
        public virtual Company.Companies UserCompany { get; set; }

        [Required(ErrorMessage = "Please select user role")]
        [ForeignKey("ApplicationUserRoles")]
        [Display(Name = "User role")]
        public short FkUserRoleId { get; set; }


        
        public virtual ApplicationUserRoles ApplicationUserRoles { get; set; }

        public virtual ApplicationUserSettings ApplicationUserSettings { get; set; }

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
