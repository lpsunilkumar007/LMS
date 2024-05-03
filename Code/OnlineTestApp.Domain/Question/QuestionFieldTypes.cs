using System.ComponentModel.DataAnnotations;

namespace OnlineTestApp.Domain.Question
{
    public class QuestionFieldTypes : BaseClasses.DomainBase
    {    

        [Key]
        public Enums.Question.FieldType FieldType { get; set; }

        [Required]
        [StringLength(256)]
        public string FieldDisplayName { get; set; }

        [Required]
        public short DisplayOrder { get; set; }      

        [Required]        
        [StringLength(300)]
        public string ErrorMessageRequired { get; set; }

        [StringLength(300)]
        public string RegularExpression { get; set; }

        [StringLength(300)]
        public string ErrorMessageRegularExpression { get; set; }

        [StringLength(300)]
        public string ValidExtensions { get; set; }

        [StringLength(300)]
        public string ErrorExtensions { get; set; }
    }
}
