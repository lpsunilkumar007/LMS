
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineTestApp.Domain.BaseClasses
{
    public abstract class DomainBase
    {
      

        DateTime _createdDateTime = DateSettings.CurrentDateTime;
        /// <summary>
        /// 
        /// </summary>
        public string CreatedDateTime_ToLongDateString
        {
            get
            {                
                return CreatedDateTime.ToString("MM/dd/yyyy HH:mm");
            }
        }

        [JsonIgnore]
        [Required]
        public DateTime CreatedDateTime
        {
            get { return _createdDateTime; }
            set { _createdDateTime = value; }
        }

        [JsonIgnore]
        public Guid LoggedInUserId
        {
            get
            {
                if (UserVariables.IsAuthenticated)
                    return UserVariables.LoggedInUserId;
                else
                    return Guid.Empty;
            }
        }
    }
}
