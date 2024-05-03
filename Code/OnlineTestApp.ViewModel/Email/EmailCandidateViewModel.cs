using OnlineTestApp.Domain.Candidate;
using System;
using System.Collections.Generic;

namespace OnlineTestApp.ViewModel.Email
{
    public class EmailCandidateViewModel : BaseClasses.SearchDomainBase
    {
        public EmailCandidateViewModel()
        {
            SendEmail = new Domain.Email.SendEmail(null);
        }

       // public List<CandidateAssignedTest> LstCandidateAssignedTest { get; set; }

        //public Guid[] EmailToCandidateIds { get; set; }

        public List<Domain.Email.EmailTemplates> LstEmailTemplates { get; set; }

        public Domain.Email.SendEmail SendEmail { get; set; }
    }
}
