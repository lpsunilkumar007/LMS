using OnlineTestApp.Domain.Candidate;
using OnlineTestApp.Domain.TestPaper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.ViewModel.Candidate
{
    public class CandidateTestResultViewModel
    {
        public TestInvitations TestInvitation { get; set; }

        public List<CandidateTestQuestions> LstCandidateTestQuestions { get; set; }
    }
}
