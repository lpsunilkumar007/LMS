using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineTestApp.Domain.Test
{
    public class TestLevel : BaseClasses.DomainBase
    {
        [Required]
        [ForeignKey("TestDetails")]
        [Key, Column(Order = 0)]
        public Guid FkTestDetailId { get; set; }

        public virtual TestDetails TestDetails { get; set; }


        [Required]
        [ForeignKey("TestQuestionLevels")]
        [Key, Column(Order = 1)]
        [Display(Name = "Level")]
        public Guid FkTestQuestionLevel { get; set; }

        public virtual LookUps.LookUpDomainValues TestQuestionLevels { get; set; }
    }
}
