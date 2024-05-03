using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestApp.Domain.Test
{
    public class TestTechnology : BaseClasses.DomainBase
    {
        [Required]
        [ForeignKey("TestDetails")]
        [Key, Column(Order = 0)]
        public Guid FkTestDetailId { get; set; }

        public virtual TestDetails TestDetails { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("QuestionTechnologies")]
        [Display(Name = "Technology")]
        public Guid FkQuestionTechnology { get; set; }

        public LookUps.LookUpDomainValues QuestionTechnologies { get; set; }
    }
}
