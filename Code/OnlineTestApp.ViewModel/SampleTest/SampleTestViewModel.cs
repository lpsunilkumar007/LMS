using OnlineTestApp.Domain.LookUps;
using OnlineTestApp.Domain.Question;
using OnlineTestApp.Domain.SampleTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.ViewModel.SampleTest
{
    public class SampleTestViewModel
    {
        public SampleTestMockups SampleTestMockups { get; set; }
        public List<Questions> Questions { get; set; }
        public List<SampleTestQuestions> SampleTestQuestions { get; set; }
        public List<LookUpDomainValues>  LookUpDomainValues { get; set; }

    }
}
