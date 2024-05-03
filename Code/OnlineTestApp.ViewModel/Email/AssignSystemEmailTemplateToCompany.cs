using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineTestApp.ViewModel.Email
{
    public class AssignSystemEmailTemplateToCompany : BaseClasses.SearchDomainBase
    {       
        public Guid EmailTemplateId { get; set; }
               
        public List<Domain.Company.Companies> LstCompany { get; set; }

        [Required(ErrorMessage = "Please select Company Id")]
        public Guid[] LstCompanyIds { get; set; }
    }
}
