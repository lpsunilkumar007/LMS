using System.ComponentModel.DataAnnotations;


namespace OnlineTestApp.Enums.User
{
    public enum UserRoles
    {
        [Display(Name = "System Admin")]
        SuperAdmin = 1,
        [Display(Name = "Company User")]
        Admin = 2,
        [Display(Name = "Company Admin")]
        CompanyAdmin = 3,

    }
}
