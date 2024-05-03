using OnlineTestApp.Domain.Question;
using OnlineTestApp.Domain.Test;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestApp.Domain.Candidate
{
    public class CandidateTestQuestionOptions : BaseClasses.DomainBase
    {
        public CandidateTestQuestionOptions()
        {
            CandidateQuestionOptionId = Guid.NewGuid();
        }

        public CandidateTestQuestionOptions(TestQuestionOptions questionOptions, Guid fkQuestionId)
        {
            CandidateQuestionOptionId = Guid.NewGuid();
            FkQuestionId = fkQuestionId;
            QuestionAnswer = questionOptions.QuestionAnswer;
            QuestionAnswerScore = questionOptions.QuestionAnswerScore;
            IsCorrect = questionOptions.IsCorrect;
            DisplayOrder = questionOptions.DisplayOrder;
        }

        [Key]
        public Guid CandidateQuestionOptionId { get; set; }

        [Required]
        [ForeignKey("Question")]
        public Guid FkQuestionId { get; set; }

        public virtual CandidateTestQuestions Question { get; set; }

        [RequiredIf("IsDeleted", false)]
        [Display(Name = "Question answer")]
        public string QuestionAnswer { get; set; }

        [RequiredIf("IsDeleted", false)]
        [Display(Name = "Score")]
        public decimal QuestionAnswerScore { get; set; }

        public bool IsCorrect { get; set; }

        [RequiredIf("IsDeleted", false)]
        [Display(Name = "Order")]
        public int DisplayOrder { get; set; }

        public string IsAnswer { get; set; }

        public bool IsCandidateAnswered { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]   


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
        public Guid? FkDeletedBy { get; set; }
        #endregion
    }
}
