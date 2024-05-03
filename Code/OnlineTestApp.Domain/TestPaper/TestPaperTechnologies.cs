using OnlineTestApp.Domain.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.Domain.TestPaper
{
    public class TestPaperTechnologies
    {
        public TestPaperTechnologies()
        {
            TestTechnologyId = Guid.NewGuid();
            if (UserVariables.IsAuthenticated)
            {
                FkCreatedBy = UserVariables.LoggedInUserId;
            }
        }

        [Key]
        public Guid TestTechnologyId { get; set; }
        [Required]
        [ForeignKey("TestPaper")]
        public Guid FkTestPaperId { get; set; }
        public virtual TestPapers TestPaper { get; set; }


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
