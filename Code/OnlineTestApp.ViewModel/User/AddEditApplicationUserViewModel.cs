using System.Collections.Generic;

namespace OnlineTestApp.ViewModel.User
{
    public class AddEditApplicationUserViewModel : BaseClasses.SearchDomainBase
    {
        public Domain.User.ApplicationUsers ApplicationUsers { get; set; }

        public List<Domain.User.ApplicationUserRoles> LstApplicationUserRoles { get; set; }

        public List<Domain.Company.Companies> LstCompanies { get; set; }
    }
}
