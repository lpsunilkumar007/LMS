using OnlineTestApp.Domain.Candidate;
using OnlineTestApp.Domain.TestPaper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OnlineTestApp.Domain.TestPaper
{
    public class TestInvitations : BaseClasses.DomainBase
    {
        public TestInvitations()
        {
            TestInvitationId = Guid.NewGuid();
            if (UserVariables.IsAuthenticated)
            {
                FkCreatedBy = UserVariables.LoggedInUserId;
            }
        }
        [Key]
        public Guid TestInvitationId { get; set; }

        [ForeignKey("TestPaper")]
        public Guid FkTestPaperId { get; set; }
        public virtual TestPapers TestPaper { get; set; }
        public Boolean IsAttempted { get; set; }
        public string Email { get; set; }
        public string TestTocken { get; set; }
        public List<CandidateTestQuestions> LstCandidateTestQuestions { get; set; }
        public string TestName { get; set; }

        public bool IsNegativeMarking { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalAnsweredQuestions { get; set; }
        public int TotalCorrectAnswers { get; set; }
        ///public int TotalQuestionsSkipped { get; set; }
        public int TotalWrongAnswers { get; set; }
        public int TotalPartialAnswers { get; set; }
        public int TotalCorrectPartialAnswers { get; set; }
        public decimal TotalCandidateScoreObtained { get; set; }
        public decimal TotalScore { get; set; }
        public decimal PassingPercentage { get; set; }
        public bool IsTestFinished { get; set; }
        public bool IsPassed { get; set; }
        [ForeignKey("CreatedByUser")]
        public Guid? FkCreatedBy { get; set; }
        public Guid? FkModefiedBy { get; set; }
        public virtual User.ApplicationUsers CreatedByUser { get; set; }

       
        public string CandidateName { get; set; }
        public string Mobile  { get; set; }

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
