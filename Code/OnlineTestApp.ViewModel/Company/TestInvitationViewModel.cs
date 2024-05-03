using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.ViewModel.Company
{
    public class TestInvitationViewModel
    {
        [Required(ErrorMessage = "Please enter email address")]      
        public string EmailFromEmailAddress { get; set; }

        public Guid FkTestPaperId { get; set; }

    }
}
