using OnlineTestApp.Domain.Question;
using OnlineTestApp.Domain.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.Domain.SampleTest
{
    public class SampleTestQuestions : BaseClasses.DomainBase
    {

        public SampleTestQuestions()
        {
            SampleTestQuestionId = Guid.NewGuid();

            if (UserVariables.IsAuthenticated)
            {
                FkCreatedBy = UserVariables.LoggedInUserId;
            }
        }

        [Key]
        public Guid SampleTestQuestionId { get; set; }


        [ForeignKey("Question")]
        public Guid? FkQuestionId { get; set; }
        public virtual Questions Question { get; set; }



        [ForeignKey("SampleTestMockup")]
        public Guid? FkSampleTestMockUpId { get; set; }
        public virtual SampleTestMockups SampleTestMockup { get; set; }
      
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
