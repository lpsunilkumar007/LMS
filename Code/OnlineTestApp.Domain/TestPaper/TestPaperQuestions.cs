using OnlineTestApp.Domain.Question;
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
    public class TestPaperQuestions : BaseClasses.DomainBase
    {
        public TestPaperQuestions()
        {
            TestQuestionId = Guid.NewGuid();

            if (UserVariables.IsAuthenticated)
            {
                FkCreatedBy = UserVariables.LoggedInUserId;
            }
        }

        [Key]
        public Guid TestQuestionId { get; set; }
        [ForeignKey("Question")]
        public Guid? FkQuestionId { get; set; }
        public virtual Questions Question { get; set; }

        [ForeignKey("TestPaper")]
        public Guid? FkTestPaperId { get; set; }
        public virtual TestPapers TestPaper { get; set; }

        public Guid? FkQuestionTechnologyId { get; set; }
        [NotMapped]
        public string QuestionTech { set; get; }

        [ForeignKey("CreatedByUser")]
        public Guid? FkCreatedBy { get; set; }

        public Guid? FkModefiedBy { get; set; }

       

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
