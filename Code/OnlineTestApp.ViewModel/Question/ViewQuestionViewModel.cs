using System;
using System.Collections.Generic;

namespace OnlineTestApp.ViewModel.Question
{
    public class ViewQuestionViewModel : BaseClasses.SearchDomainBase
    {
        public List<Domain.Question.Questions> LstQuestions { get; set; }
        
        public string QuestionTitle { get; set; }

        public Guid? QuestionTechnology { get; set; }

        public Guid? QuestionLevel { get; set; }

        public List<Domain.LookUps.LookUpDomainValues> LstQuestionTechnology { get; set; }

        public List<Domain.LookUps.LookUpDomainValues> LstQuestionLevel { get; set; }
    }
}

