using OnlineTestApp.Domain.TestPaper;
using OnlineTestApp.ViewModel.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.ViewModel.Company
{
    public class CandidateTestReportsViewModel : SearchDomainBase
    {
        public List<TestPaperReport> LstTestPaperReport { get; set; }

    }
    public class TestPaperReport
    {
        public Guid TestpaperId { get; set; }
        public string TestName { get; set; }
        public decimal Total { get; set; }
        public decimal TotalAttempted { get; set; }
        public int TotalPassed { get; set; }
        public int TotalFail { get; set; }
        public decimal PerAttempt
        {
            get
            {
                return decimal.Round(((TotalAttempted / Total) * 100), 2, MidpointRounding.AwayFromZero);
            }
        }
    }
}
