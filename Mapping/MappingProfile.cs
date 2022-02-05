using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using MyAspCoreFinalProject.Models;
using MyAspCoreFinalProject.ViewModel;

namespace MyAspCoreFinalProject.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientVM>();
            CreateMap<PatientVM, Patient>();
        }
    }
}
