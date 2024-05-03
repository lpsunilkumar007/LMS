using OnlineTestApp.Domain.LookUps;
using OnlineTestApp.Domain.Question;
using OnlineTestApp.Domain.Test;
using OnlineTestApp.Domain.TestPaper;
using OnlineTestApp.Domain.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineTestApp.Domain.Candidate
{
    public class CandidateTestQuestions : BaseClasses.DomainBase
    {
        public CandidateTestQuestions()
        {
            CandidateTestQuestionId = Guid.NewGuid();
            TotalScore = 0;
            if (UserVariables.IsAuthenticated)
            {
                FkCreatedBy = UserVariables.LoggedInUserId;
            }
            MaxScore = 0;
            TotalTime = 30;
        }

        public CandidateTestQuestions(TestQuestions questions, Guid candidateTestDetailId, int displayOrder)
        {
            CandidateTestQuestionId = Guid.NewGuid();
            // FkCandidateAssignedTestId = candidateTestDetailId;
            FieldType = questions.FieldType;
            TotalScore = questions.TotalScore;
            DisplayOrder = displayOrder;
            QuestionTitle = questions.QuestionTitle;
            QuestionDescription = questions.QuestionDescription;
            TotalTime = questions.TotalTime;
            CanSkipQuestion = questions.CanSkipQuestion;
            MaxScore = questions.MaxScore;
            NegativeMarks = questions.NegativeMarks;
            ErrorMessage = questions.ErrorMessage;
            RegularExpression = questions.RegularExpression;
            ErrorMessageRegularExpression = questions.ErrorMessageRegularExpression;
            ValidExtensions = questions.ValidExtensions;
            ErrorExtensions = questions.ErrorExtensions;
            FkCreatedBy = UserVariables.LoggedInUserId;
            LstCandidateTestQuestionOptions = new List<CandidateTestQuestionOptions>();
            TotalOptions = questions.TotalOptions;
        }

        // [Required] not in use for now 
        // [ForeignKey("CandidateTestDetails")]
        // public Guid? FkCandidateAssignedTestId { get; set; }

        //  public virtual CandidateTestDetails CandidateTestDetails { get; set; }

        [Key]
        public Guid CandidateTestQuestionId { get; set; }


        [Required]
        [ForeignKey("TestInvitations")]
        public Guid FkTestInvitationId { get; set; }
        public virtual TestInvitations TestInvitations { get; set; }


        [Required]
        [ForeignKey("TestPapers")]
        public Guid FkTestPaperId { get; set; }

        public virtual TestPapers TestPapers { get; set; }

        [ForeignKey("QuestionFieldType")]
        [Required(ErrorMessage = "Please select field type")]
        [Display(Name = "Control")]
        public Enums.Question.FieldType FieldType { get; set; }

        public virtual QuestionFieldTypes QuestionFieldType { get; set; }

        [Required]
        [Display(Name = "Total score")]
        public decimal TotalScore { get; set; }

        [Display(Name = "Display order")]
        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        [Display(Name = "Question title")]
        public string QuestionTitle { get; set; }

        [Required]
        [Display(Name = "Question description")]
        public string QuestionDescription { get; set; }

        [Required]
        [Display(Name = "Total time")]
        public short TotalTime { get; set; }

        [Required]
        public bool CanSkipQuestion { get; set; }

        [Required]
        [Display(Name = "Max score")]
        public decimal MaxScore { get; set; }

        [Required]
        public bool NegativeMarks { get; set; }

        [StringLength(300)]
        public string ErrorMessage { get; set; }

        [StringLength(300)]
        public string RegularExpression { get; set; }

        [StringLength(300)]
        public string ErrorMessageRegularExpression { get; set; }

        [StringLength(300)]
        public string ValidExtensions { get; set; }

        [StringLength(300)]
        public string ErrorExtensions { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        // [ForeignKey("CreatedByUser")]
        public Guid? FkCreatedBy { get; set; }

        //public virtual ApplicationUsers CreatedByUser { get; set; }

        public virtual List<CandidateTestQuestionOptions> LstCandidateTestQuestionOptions { get; set; }

        public int TotalOptions { get; set; }

        public bool IsFullyCorrectAnswered { get; set; }

        public bool IsPartiallyCorrectAnswered { get; set; }

        public decimal TotalCandidateScoreObtained { get; set; }

        public bool IsQuestionAnswered { get; set; }

        public bool IsSkippedAnswered { get; set; }

        public bool IsNegativeMarking { get; set; }

        [NotMapped]
        public string IsSelectedAnswer { get; set; }


        [ForeignKey("QuestionTechnology")]
        public Guid FkQuestionTechnologyId { get; set; }

        public virtual LookUpDomainValues QuestionTechnology { get; set; }

        #region 
        /// <summary>
        /// 
        /// </summary>
        bool isActive = true, isDeleted = false;
        private TestQuestions testQuestionsToAssign;
        private Guid testDetailId;
        private int i;

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
