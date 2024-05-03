using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineTestApp.ViewModel.Test
{
    public class AddNewTestViewModel : BaseClasses.SearchDomainBase
    {
        public List<Domain.LookUps.LookUpDomainValues> LstTestLevel { get; set; }

        [Required(ErrorMessage = "Please select technology")]
        public Guid?[] QuestionTechnology { get; set; }

        [Required(ErrorMessage = "Please select level")]
        public Guid?[] QuestionLevel { get; set; }
        public List<Domain.LookUps.LookUpDomainValues> LstTestTechnology { get; set; }
        public Domain.Test.TestDetails TestDetails { get; set; }
        public List<ViewPreparedTestBaches> LstViewPreparedTestBaches { get; set; }
        public Domain.Email.SendEmail InviteForTest { get; set; }

       
    }
}
