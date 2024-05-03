using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineTestApp.Domain.User
{
    public class ApplicationUserRoles : BaseClasses.DomainBase
    {
        [Key]
        public short UserRoleId { get; set; }

        [Required]
        [Index("IX_Unique_ApplicationUserRoles_UserRoleName", IsUnique = true)]
        [StringLength(256)]
        [Column(TypeName = "varchar")]
        [Display(Name = "User role name")]
        public string UserRoleName { get; set; }

        [Required]
        [Index("IX_Unique_ApplicationUserRoles_UserRoleCode", IsUnique = true)]
        public Enums.User.UserRoles UserRoleCode { get; set; }

        [Required]
        [StringLength(2000)]
        [Column(TypeName = "varchar")]
        [Display(Name = "User role description")]
        public string UserRoleDescription { get; set; }

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
