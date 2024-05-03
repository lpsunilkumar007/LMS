using System;
using System.Collections.Generic;

namespace OnlineTestApp.ViewModel.Company
{    
    public class ViewCompanyViewModel : BaseClasses.SearchDomainBase
    {
        public List<Domain.Company.Companies> LstCompanies { get; set; }

        public Guid[] LstCompanyId { get; set; }
    }
}


