using System;

namespace OnlineTestApp.ViewModel.Test
{
    public class ViewPreparedTestBaches : BaseClasses.SearchDomainBase
    {
        public Guid TempTestId { get; set; }
        public string QuestionIds { get; set; }
        public int TotalTime { get; set; }
        public int TotalQuestions { get; set; }
        public bool IsSelected { get; set; }
    }
}
