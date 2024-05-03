using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestApp.Domain.User
{
    public class ApplicationUserSettings : BaseClasses.DomainBase
    {
        public ApplicationUserSettings()
        {
            IsMenuOpen = true;
        }
        [Key]
        [ForeignKey("ApplicationUsers")]
        public Guid ApplicationUserId { get; set; }

        public bool IsMenuOpen { get; set; }


        public virtual ApplicationUsers ApplicationUsers { get; set; }
    }
}
