using System;
using System.Collections.Generic;

namespace OnlineTestApp.ViewModel.User
{
    public class ViewApplicationUserViewModel : BaseClasses.SearchDomainBase
    {
        public List<Domain.Company.Companies> LstCompanies { get; set; }

        public Guid? CompanyId { get; set; }

        public List<Domain.User.ApplicationUsers> LstApplicationUsers { get; set; }
    }
}
