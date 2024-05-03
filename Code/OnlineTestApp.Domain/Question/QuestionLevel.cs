using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineTestApp.Domain.Question
{
    public class QuestionLevel : BaseClasses.DomainBase
    {
        [Required]
        [ForeignKey("Question")]
        [Key, Column(Order = 0)]
        public Guid FkQuestionId { get; set; }

        public virtual Questions Question { get; set; }

        [Required]
        [ForeignKey("QuestionLevels")]
        [Key, Column(Order = 1)]
        [Display(Name = "Level")]
        public Guid FkQuestionLevel { get; set; }

        public virtual LookUps.LookUpDomainValues QuestionLevels { get; set; }
    }
}
