using System.ComponentModel.DataAnnotations;
namespace OnlineTestApp.ViewModel.UserMemberShip
{
    public class UserLoginViewModel
    {
        [StringLength(100, ErrorMessage = "Username cannot be longer than 100 characters")]
        [Required(ErrorMessage = "Please enter usename")]
        public string UserName { get; set; }

        [StringLength(100, ErrorMessage = "Password cannot be longer than 100 characters")]
        [Required(ErrorMessage = "Please enter password")]
        public string UserPassword { get; set; }
        public bool RememberMe { get; set; }

    }
}
