using OnlineTestApp.Domain.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestApp.Domain.Candidate
{
    public class CandidateTestDetails : BaseClasses.DomainBase
    {
        public CandidateTestDetails()
        {
            CandidateTestDetailId = Guid.NewGuid();
            StartTime = DateSettings.CurrentDateTime;
        }

        [Key]
        public Guid CandidateTestDetailId { get; set; }
       
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


        public bool IsTestExpired
        {
            get
            {
                return EndTime <= DateSettings.CurrentDateTime;
            }
        }

        [Key]
        [Index("IX_Unique_CandidateTestDetails_FkCandidateAssignedTestId", IsUnique = true)]
        [ForeignKey("CandidateAssignedTest")]
        public Guid FkCandidateAssignedTestId { get; set; }
        public virtual CandidateAssignedTest CandidateAssignedTest { get; set; }



        public int TotalQuestions { get; set; }

        public int TotalAnsweredQuestions { get; set; }

        public int TotalCorrectAnswers { get; set; }

        public int TotalWrongAnswers { get; set; }

        public int TotalPartialAnswers { get; set; }

        public int TotalCorrectPartialAnswers { get; set; }

        public bool IsCompleted { get; set; }

        public decimal TotalCandidateScoreObtained { get; set; }

        public decimal TotalScore { get; set; }
        
        public decimal MaxScore { get; set; }
        
     //   public virtual List<CandidateTestQuestions> LstCandidateTestQuestions { get; set; }

        public string StrTotalTime
        {
            get
            {
                var TotalTime = EndTime-StartTime;
                return string.Format("{0:%h}", TotalTime.Hours.ToString()) + ":" + string.Format("{0:%m}", TotalTime.Minutes.ToString());
            }
        }

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
