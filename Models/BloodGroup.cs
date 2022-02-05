using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspCoreFinalProject.Models
{
    public class BloodGroup
    {
        [Key]
        public int BloodGroupID { get; set; }
        public string BloodGroupName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Donor> Donors { get; set; }
    }
}
