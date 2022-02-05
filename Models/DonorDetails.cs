using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspCoreFinalProject.Models
{
    public class DonorDetails
    {
        public DonorDetails()
        {
        }

        [Key]
        public int DonorDetailsId { get; set; }

        [ForeignKey("BloodDonor")]
        public int BloodDonorId { get; set; }
        public virtual BloodDonor Donor { get; private set; }

        public string OrganizationName { get; set; }
        

        public int DonateTime { get; set; }
    }
}
