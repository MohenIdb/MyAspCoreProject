using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspCoreFinalProject.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string Email { get; set; }
        public string CellPhoneNo { get; set; }
        public DateTime DOB { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
