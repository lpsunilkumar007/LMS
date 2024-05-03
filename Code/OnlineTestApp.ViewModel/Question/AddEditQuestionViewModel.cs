using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineTestApp.ViewModel.Question
{
    public class AddEditQuestionViewModel : BaseClasses.SearchDomainBase
    {
        public Domain.Question.Questions Questions { get; set; }

        [Required(ErrorMessage = "Please select question level")]
        public Guid[] QuestionLevel { get; set; }

        [Required(ErrorMessage = "Please select question technology")]
        public Guid[] QuestionTechnology { get; set; }
        public List<Domain.LookUps.LookUpDomainValues> LstQuestionTechnology { get; set; }
        public List<Domain.LookUps.LookUpDomainValues> LstQuestionLevel { get; set; }
        public List<Domain.Question.QuestionFieldTypes> LstQuestionFieldTypes { get; set; }
    }
}
