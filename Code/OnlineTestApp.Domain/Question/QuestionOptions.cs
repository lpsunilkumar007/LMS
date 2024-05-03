using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestApp.Domain.Question
{
    public class QuestionOptions : BaseClasses.DomainBase
    {
        public QuestionOptions()
        {
            QuestionOptionId = Guid.NewGuid();
        }

        [Key]
        public Guid QuestionOptionId { get; set; }

        [Required]
        [ForeignKey("Question")]
        public Guid FkQuestionId { get; set; }

        public virtual Questions Question { get; set; }

        //[Required]
        [RequiredIf("IsDeleted",false)]
        [Display(Name = "Question answer")]
        public string QuestionAnswer { get; set; }

        [RequiredIf("IsDeleted", false)]
        [Display(Name = "Score")]
        public decimal QuestionAnswerScore { get; set; }

        public bool IsCorrect { get; set; }

        [RequiredIf("IsDeleted", false)]
        [Display(Name = "Order")]
        public int DisplayOrder { get; set; } 
        #region 
        /// <summary>
        /// 
        /// </summary>
        bool  isDeleted = false;

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
