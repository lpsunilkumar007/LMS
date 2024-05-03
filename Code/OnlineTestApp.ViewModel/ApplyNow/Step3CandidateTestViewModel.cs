using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.ViewModel.ApplyNow
{
     public class Step3CandidateTestViewModel : ApplyNowBase
    {
        // public Domain.Test.TempTests TempTests { get; set; }
        public Domain.Test.TestDetails TestDetails { get; set; }

        public Domain.Candidate.CandidateTestDetails CandidateTestDetails { get; set; }

        public Domain.Candidate.CandidateTestQuestions CandidateTestQuestions { get; set; }
    }
}
