using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MyAspCoreFinalProject.Data;
using MyAspCoreFinalProject.Mapping;
using MyAspCoreFinalProject.Models;
using MyAspCoreFinalProject.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MyAspCoreFinalProject.Controllers
{
    public class PatientController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public PatientController(IMapper mapper, ApplicationDbContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patients.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientVM patientVM)
        {
            var patient = _mapper.Map<PatientVM, Patient>(patientVM);
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var singlePatient = await _context.Patients.FindAsync(id);
            var patient = _mapper.Map<Patient, PatientVM>(singlePatient);
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientVM patientVM, int id)
        {
            var singlePatient = await _context.Patients.FindAsync(id);
            //_mapper.Map<PatientVM, Patient>(patientVM, singlePatient);
            _mapper.Map(patientVM, singlePatient);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var singlePatient = await _context.Patients.FindAsync(id);
            var patient = _mapper.Map<Patient, PatientVM>(singlePatient);
            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var singlePatient = await _context.Patients.FindAsync(id);
            _context.Remove(singlePatient);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var singlePatient = await _context.Patients.FindAsync(id);
            var patient = _mapper.Map<Patient, PatientVM>(singlePatient);
            return View(patient);
        }
    }
}
