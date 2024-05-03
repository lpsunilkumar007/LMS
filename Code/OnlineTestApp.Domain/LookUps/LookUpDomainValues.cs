using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineTestApp.Domain.User;

namespace OnlineTestApp.Domain.LookUps
{
    public class LookUpDomainValues : BaseClasses.DomainBase
    {
        public LookUpDomainValues()
        {
            LookUpDomainValueId = Guid.NewGuid();
            DisplayOrder = 1;
            CanEditLookUpDomainValue = CanEditLookUpDomainValueText = true;
            CanDeleteRecord = true;
        }

        [Key]
        public Guid LookUpDomainValueId { get; set; }

        [ForeignKey("LookUpDomain")]
        public Guid FkLookUpDomainId { get; set; }

        public virtual LookUpDomains LookUpDomain { get; set; }

        string _lookUpDomainCode = "", _lookUpDomainValue = "";

        [StringLength(500, ErrorMessage = "Code cannot be longer than 500 characters")]
        [Column(TypeName = "varchar")]
        [Required]
        public string LookUpDomainCode
        {
            get
            {
                if (string.IsNullOrEmpty(_lookUpDomainCode) || string.IsNullOrWhiteSpace(_lookUpDomainCode))
                {
                    return "----";
                    //return LookUpDomainValueText;
                }
                return _lookUpDomainCode;
            }
            set
            {
                _lookUpDomainCode = value;
            }

        }

        [Required(ErrorMessage = "Please enter value")]
        public string LookUpDomainValue
        {
            get
            {
                if (string.IsNullOrEmpty(_lookUpDomainValue) || string.IsNullOrWhiteSpace(_lookUpDomainValue))
                {
                    return LookUpDomainValueText;
                }
                return _lookUpDomainValue;
            }
            set
            {
                _lookUpDomainValue = value;
            }
        }

        string _lookUpDomainValueText = "";


        [Required(ErrorMessage = "Please enter text")]
        public string LookUpDomainValueText
        {
            get
            {
                return _lookUpDomainValueText.ToFirstLetterCapitalize();
            }
            set
            {
                _lookUpDomainValueText = value;
            }
        }

        [Required(ErrorMessage = "Please enter value")]
        public bool CanEditLookUpDomainValue { get; set; }

        [Required(ErrorMessage = "Please enter text")]
        public bool CanEditLookUpDomainValueText { get; set; }

        [Required]
        public bool CanDeleteRecord { get; set; }

        [Required]
        [Range (1, 2147483647, ErrorMessage = "Display Order should be between 1 and 2147483647")]
        public int DisplayOrder { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid FkCreatedBy { get; set; }
        public virtual ApplicationUsers CreatedByUser { get; set; }

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
