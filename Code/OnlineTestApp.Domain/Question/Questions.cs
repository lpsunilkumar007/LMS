using OnlineTestApp.Domain.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestApp.Domain.Question
{
    public class Questions : BaseClasses.DomainBase
    {

        public Questions()
        {
            QuestionId = Guid.NewGuid();
            TotalScore = 0;
            if (UserVariables.IsAuthenticated)
            {
                FkCreatedBy = UserVariables.LoggedInUserId;
            }
            MaxScore = 0;
            TotalTime = 30;
        }

        [Key]
        public Guid QuestionId { get; set; }

        [ForeignKey("QuestionFieldType")]
        [Required(ErrorMessage = "Please select field type")]
        [Display(Name = "Control")]
        public Enums.Question.FieldType FieldType { get; set; }

        public virtual QuestionFieldTypes QuestionFieldType { get; set; }

        [Required]
        [Display(Name = "Total score")]        
        public decimal TotalScore { get; set; }

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


        //[StringLength(300)]
        //public string ErrorMessage { get; set; }

        //[StringLength(300)]
        //public string RegularExpression { get; set; }

        //[StringLength(300)]
        //public string ErrorMessageRegularExpression { get; set; }

        //[StringLength(300)]
        //public string ValidExtensions { get; set; }

        //[StringLength(300)]
        //public string ErrorExtensions { get; set; }

        public virtual List<QuestionTechnology> LstQuestionTechnology { get; set; }

        public virtual List<QuestionLevel> LstQuestionLevel { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid FkCreatedBy { get; set; }

        public virtual ApplicationUsers CreatedByUser { get; set; }

        public virtual List<QuestionOptions> LstQuestionOptions { get; set; }

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
