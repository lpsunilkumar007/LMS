using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.ViewModel.UserMemberShip
{
    public class CompanyUserViewModel
    {
        [StringLength(256, ErrorMessage = "Email address cannot be longer than 256 characters")]
        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid email address")]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

      
        public string UserName { get; set; }

        [StringLength(100, ErrorMessage = "Password cannot be longer than 100 characters")]
        [Required(ErrorMessage = "Please enter password")]
        public string UserPassword { get; set; }
        public bool RememberMe { get; set; }
    }
}
