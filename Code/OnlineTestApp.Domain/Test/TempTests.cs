using OnlineTestApp.Domain.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestApp.Domain.Test
{
    public class TempTests : BaseClasses.DomainBase
    {
        [Key]
        public Guid TempTestId { get; set; }

        [Column(TypeName = "varchar")]
        public string QuestionIds { get; set; }

        public int TotalTime { get; set; }

        public int TotalQuestions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("CreatedByUser")]
        public Guid FkCreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ApplicationUsers CreatedByUser { get; set; }

    }
}
