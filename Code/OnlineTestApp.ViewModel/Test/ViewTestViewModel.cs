using System;
using System.Collections.Generic;

namespace OnlineTestApp.ViewModel.Test
{
    public class ViewTestViewModel : BaseClasses.SearchDomainBase
    {
        public List<Domain.LookUps.LookUpDomainValues> LstTestLevel { get; set; }

        public List<Domain.LookUps.LookUpDomainValues> LstTestTechnology { get; set; }
        
        public string TestTitle { get; set; }

        public Guid? TestTechnology { get; set; }

        public Guid? TestLevel { get; set; }

        public List<Domain.Test.TestDetails> LstTestDetails { get; set; }
    }
}

