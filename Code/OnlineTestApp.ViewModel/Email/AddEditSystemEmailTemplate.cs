using OnlineTestApp.Domain.Email;
using System.Collections.Generic;

namespace OnlineTestApp.ViewModel.Email
{
    public class AddEditSystemEmailTemplate : BaseClasses.SearchDomainBase
    {
        public AddEditSystemEmailTemplate()
        {
            EmailTemplates = new EmailTemplates();
        }
        public EmailTemplates EmailTemplates { get; set; }

        public List<Domain.Company.Companies> LstCompany { get; set; }        
    }
}
