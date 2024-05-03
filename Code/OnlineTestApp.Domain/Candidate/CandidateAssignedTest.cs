using OnlineTestApp.Domain.Test;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestApp.Domain.Candidate
{
    public class CandidateAssignedTest : BaseClasses.DomainBase
    {
        public CandidateAssignedTest()
        {
            CandidateAssignedTestId = Guid.NewGuid();
        }

        [Key]
        public Guid CandidateAssignedTestId { get; set; }


        [Index(IsUnique = true), Column(Order = 0)]
        [ForeignKey("Candidates")]
        public Guid CandidateId { get; set; }


        public virtual Candidates Candidates { get; set; }

        [Index(IsUnique = true), Column(Order = 1)]
        [ForeignKey("TestDetails")]
        public Guid TestDetailId { get; set; }

        public virtual TestDetails TestDetails { get; set; }

        [Required]
        [StringLength(256)]
        [Column(TypeName = "VARCHAR")]
        [Index("IX_Unique_CandidateAssignedTest_TestReferenceNumber", IsUnique = true)]
        public string TestReferenceNumber { get; set; }

        public virtual CandidateTestDetails CandidateTestDetails { get; set; }


        #region 
        /// <summary>
        /// 
        /// </summary>
        bool isDeleted = false;
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeletedDateDateTime
        {
            get; set;
        }


        public Guid? FkDeletedBy { get; set; }
        #endregion

    }
}
