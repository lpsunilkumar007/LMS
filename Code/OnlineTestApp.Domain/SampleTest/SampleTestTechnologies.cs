using OnlineTestApp.Domain.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.Domain.SampleTest
{
    public class SampleTestTechnologies : BaseClasses.DomainBase
    {
        public SampleTestTechnologies()
        {
            SampleTestTechnologyId = Guid.NewGuid();
            if (UserVariables.IsAuthenticated)
            {
                FkCreatedBy = UserVariables.LoggedInUserId;
            }
        }

        [Key]
        public Guid SampleTestTechnologyId { get; set; }
        [Required]
        [ForeignKey("SampleTestMockups")]
        public Guid FkSampleTestMockUpId { get; set; }
        public virtual SampleTestMockups SampleTestMockups { get; set; }       


        [Required]
        [ForeignKey("Technologies")]
        public Guid FkTechnology { get; set; }
        public virtual LookUps.LookUpDomainValues Technologies { get; set; }      


        [ForeignKey("CreatedByUser")]
        public Guid? FkCreatedBy { get; set; }
        public virtual ApplicationUsers CreatedByUser { get; set; }
       

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
