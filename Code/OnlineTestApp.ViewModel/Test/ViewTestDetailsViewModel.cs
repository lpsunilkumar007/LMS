using OnlineTestApp.Domain.Candidate;
using OnlineTestApp.Domain.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.ViewModel.Test
{
    public class ViewTestDetailsViewModel:BaseClasses.SearchDomainBase
    {
        public Guid TestDetailId { get; set; }

        public TestDetails TestDetails { get; set; }

        //public List<CandidateAssignedTest> LstCandidateAssignedTest { get; set; }
        //public List<CandidateTestDetails> LstCandidateAssignedTest { get; set; }
    }
}
