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
    public class SampleTestMockups : BaseClasses.DomainBase
    {
        public SampleTestMockups()
        {
            SampleTestMockUpId = Guid.NewGuid();
            if (UserVariables.IsAuthenticated)
            {
                FkCreatedBy = UserVariables.LoggedInUserId;
            }
        }

        [Key]
        public Guid SampleTestMockUpId { get; set; }

        public string MockupName { get; set; }//autogenerate  Mockup-A,B,C,D
        public string SampleTestBatch { get; set; } //same for all the test genearted (Mockup-A,B,C,D  => some sample sample batch)

        [Required(ErrorMessage = "Please enter total questions")]
        public int TotalQuestions { get; set; }

        [Required(ErrorMessage = "Please enter total marks")]
        public int TotalMarks { get; set; }

        [Required(ErrorMessage = "Please enter duration")]
        public int Duration { get; set; }

        public bool IsNegativeMarking { get; set; }
      
        [ForeignKey("TestLevels")]
        [Required(ErrorMessage = "Please select test level")]
        public Guid FkTestLevel { get; set; }
        public decimal PassingPercentage { get; set; }
        public virtual LookUps.LookUpDomainValues TestLevels { get; set; }


        [ForeignKey("CreatedByUser")]
        public Guid? FkCreatedBy { get; set; }    
       // [ForeignKey("ModefiedByUser")]
        public Guid? FkModefiedBy { get; set; }
       // public virtual ApplicationUsers ModefiedByUser { get; set; }
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
