using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using MyAspCoreFinalProject.Models;

namespace MyAspCoreFinalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BloodGroup> BloodGroups { get; set; }
        public virtual DbSet<Donor> Donors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }

        public virtual DbSet<BloodDonor> BloodDonors { get; set; }
        public virtual DbSet<DonorDetails> DonorDetails { get; set; }
    }
}
