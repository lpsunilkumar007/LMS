using System.ComponentModel.DataAnnotations;

namespace OnlineTestApp.ViewModel.MyAccount
{
    public class ChangePasswordViewModel : BaseClasses.SearchDomainBase
    {
        [Required(ErrorMessage = "Please enter old password")]
        [StringLength(50, ErrorMessage = "Old password cannot be longer than 50 characters")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please enter new password")]
        [StringLength(50, ErrorMessage = "New password cannot be longer than 50 characters")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please enter confirm password")]
        [CompareAttribute("NewPassword", ErrorMessage = "Password and confirm password does not match")]
        [StringLength(50, ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
