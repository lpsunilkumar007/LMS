using OnlineTestApp.Domain.Question;
using OnlineTestApp.Domain.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineTestApp.Domain.Test
{
    public class TestQuestions : BaseClasses.DomainBase
    {
        public TestQuestions()
        {
            TestQuestionId = Guid.NewGuid();
            TotalScore = 0;
            if (UserVariables.IsAuthenticated)
            {
                FkCreatedBy = UserVariables.LoggedInUserId;
            }
            MaxScore = 0;
            TotalTime = 30;
        }
        
        public TestQuestions(Questions questions, Guid testDetailId, int displayOrder)
        {
            TestQuestionId = Guid.NewGuid();
            FkTestDetailId = testDetailId;
            FieldType = questions.FieldType;
            TotalScore = questions.TotalScore;
            DisplayOrder = displayOrder;
            QuestionTitle = questions.QuestionTitle;
            QuestionDescription = questions.QuestionDescription;
            TotalTime = questions.TotalTime;
            CanSkipQuestion = questions.CanSkipQuestion;
            MaxScore = questions.MaxScore;
            NegativeMarks = questions.NegativeMarks;
           // ErrorMessage = questions.ErrorMessage;
           // RegularExpression = questions.RegularExpression;
            //ErrorMessageRegularExpression = questions.ErrorMessageRegularExpression;
           // ValidExtensions = questions.ValidExtensions;
           // ErrorExtensions = questions.ErrorExtensions;
            FkCreatedBy = UserVariables.LoggedInUserId;
            LstTestQuestionOptions = new List<TestQuestionOptions>();
            TotalOptions = questions.TotalOptions;
        }
        

        [Key]
        public Guid TestQuestionId { get; set; }

        [Required]
        [ForeignKey("TestDetails")]
        public Guid FkTestDetailId { get; set; }

        public virtual TestDetails TestDetails { get; set; }

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

        [ForeignKey("CreatedByUser")]
        public Guid FkCreatedBy { get; set; }

    
        public virtual ApplicationUsers CreatedByUser { get; set; }

        public virtual List<TestQuestionOptions> LstTestQuestionOptions { get; set; }

        #region 
        /// <summary>
        /// 
        /// </summary>
        bool isActive = true, isDeleted = false;

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


        public int TotalOptions { get; set; }
    }
}
