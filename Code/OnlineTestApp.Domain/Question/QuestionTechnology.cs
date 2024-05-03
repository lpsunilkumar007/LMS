using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestApp.Domain.Question
{
    public class QuestionTechnology : BaseClasses.DomainBase
    {
        [Required]
        [ForeignKey("Question")]
        [Key, Column(Order = 0)]
        public Guid FkQuestionId { get; set; }

        public virtual Questions Question { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("QuestionTechnologies")]
        [Display(Name = "Technology")]
        public Guid FkQuestionTechnology { get; set; }

        public LookUps.LookUpDomainValues QuestionTechnologies { get; set; }
    }
}
