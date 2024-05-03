using OnlineTestApp.Domain.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestApp.Domain.Test
{
    public class TestDetails : BaseClasses.DomainBase
    {
        public TestDetails()
        {
            TestDetailId = Guid.NewGuid();
            if (UserVariables.IsAuthenticated)
            {
                FkCreatedBy = UserVariables.LoggedInUserId;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid TestDetailId { get; set; }

        [Required(ErrorMessage = "Please enter test title")]
        [StringLength(500, ErrorMessage = "Test title name cannot be longer than 500 characters")]
        public string TestTitle { get; set; }


        //[StringLength(256)]       
        //public string TestReferenceNumber { get; set; }

        public string TestIntroductoryText { get; set; }

        [Required]
        [Display(Name = "Total time")]
        [Range(1, int.MaxValue, ErrorMessage = "Total time must be greater than 1")]
        public int TotalTime { get; set; }

        [Required]
        public DateTime ValidUpTo{ get; set; }

        public bool IsTestExpired
        {
            get
            {
                return ValidUpTo <= DateSettings.CurrentDateTime;
            }
        }

        [Required]
        [Display(Name = "Total score")]
        public decimal TotalScore { get; set; }

        public virtual List<TestLevel> TestLevel { get; set; }

        public virtual List<TestQuestions> LstTestQuestions { get; set; }

        public virtual List<TestTechnology> LstTestTechnology { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("CreatedByUser")]
        public Guid FkCreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ApplicationUsers CreatedByUser { get; set; }

        public int? TotalCandidates { get; set; }
        
        [Display(Name = "Max score")]
        public decimal MaxScore { get; set; }

        //public virtual List<Candidate.CandidateAssignedTest> LstCandidateAssignedTest { get; set; }


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
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeletedDateDateTime
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid? FkDeletedBy { get; set; }
        #endregion
    }
}
