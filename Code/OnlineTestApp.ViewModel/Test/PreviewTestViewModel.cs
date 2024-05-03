using System.Collections.Generic;

namespace OnlineTestApp.ViewModel.Test
{
    public class PreviewTestViewModel : BaseClasses.SearchDomainBase
    {
        public Domain.Test.TempTests TempTests { get; set; }

        public List<Domain.Question.Questions> LstQuestions { get; set; }
    }
}
