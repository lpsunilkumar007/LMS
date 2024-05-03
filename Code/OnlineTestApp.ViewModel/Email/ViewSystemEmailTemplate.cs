using System.Collections.Generic;

namespace OnlineTestApp.ViewModel.Email
{
    public class ViewSystemEmailTemplate : BaseClasses.SearchDomainBase
    {        
        //public Guid? CompanyId { get; set; }

        public List<Domain.Email.EmailTemplates> LstEmailTemplates { get; set; }

        //public List<Domain.Company.Companies> LstCompany { get; set; }
    }
}
