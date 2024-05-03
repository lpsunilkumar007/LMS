using OnlineTestApp.ViewModel.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.ViewModel.Company
{
    public class TestMockupViewModel
    {
        public List<Domain.LookUps.LookUpDomainValues> LstExperienceLevel { get; set; }

        [Required(ErrorMessage = "Please select technology")]
        public Guid?[] QuestionTechnology { get; set; }

        [Required(ErrorMessage = "Please select level")]
        public Guid?[] QuestionLevel { get; set; }
        public List<Domain.LookUps.LookUpDomainValues> LstTestTechnology { get; set; }
        public Domain.Test.TestDetails TestDetails { get; set; }

        public Guid Technology { get; set; }

    }
}
