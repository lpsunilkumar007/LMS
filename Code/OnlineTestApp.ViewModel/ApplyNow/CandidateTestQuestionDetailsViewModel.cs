using System;

namespace OnlineTestApp.ViewModel.ApplyNow
{
    public class CandidateTestQuestionDetailsViewModel : ApplyNowBase
    {
        public Guid CandidateTestDetailsId { get; set; }

        public Domain.Candidate.CandidateTestQuestions CandidateTestQuestions { get; set; }
    }
}
