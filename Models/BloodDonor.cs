using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspCoreFinalProject.Models
{
    public class BloodDonor
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(150)]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = "";

        [Required]
        [Range(25, 55, ErrorMessage = "Currently,We Have no Positions Vacant for Your Age")]
        [DisplayName("Age in Years")]
        public int Age { get; set; }


        public virtual List<DonorDetails> DonorDetails { get; set; } = new List<DonorDetails>();

        public string PhotoUrl { get; set; }

        [Required(ErrorMessage = "Please choose the Profile Photo")]
        [Display(Name = "Profile Photo")]
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }
    }
}
