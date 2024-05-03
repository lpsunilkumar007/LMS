using OnlineTestApp.Domain.TestPaper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.ViewModel.Candidate
{
    public class TestDetailsViewModel
    {
        public TestPapers TestPapers { get; set; }
        public Guid FkTestInvitationId { get; set; }


        [Required(ErrorMessage = "Enter your name ")]
        public string CandidateName { get; set; }
        public string Mobile { get; set; }

    }
}
