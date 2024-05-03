using OnlineTestApp.Domain.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.Domain.Candidate
{
    public class Candidates : BaseClasses.DomainBase
    {
        public Candidates()
        {
            CandidateId = Guid.NewGuid();
        }

        [Key]
        public Guid CandidateId { get; set; }

        [StringLength(50, ErrorMessage = "")]
        public string CandidateName { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [StringLength(256)]
        [Index("IX_Unique_Candidates_CandidateEmailAddress", IsUnique = true)]
        public string CandidateEmailAddress { get; set; }
        /// 
        /// </summary>
        [ForeignKey("CreatedByUser")]       
        public Guid FkCreatedBy { get; set; }


        public Company.Companies Company { get; set; }

        [ForeignKey("Company")]
        public Guid FKCompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ApplicationUsers CreatedByUser { get; set; }

    }
}
