using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.ViewModel.Company
{
    public class CandidateTestLeftSideViewModel
    {
        public int TotalQuestions { get; set; }

        public int AttemptedQuestions { get; set; }

        public int SkippedQuestions { get; set; }

        public int PendingQuestions
        {
            get
            {
                return (TotalQuestions - (AttemptedQuestions+ SkippedQuestions));
            }           
        }
    }
}
