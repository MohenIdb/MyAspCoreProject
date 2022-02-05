using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAspCoreFinalProject.Data;
using MyAspCoreFinalProject.Models;

namespace MyAspCoreFinalProject.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Registrations.Include(r => r.Donor).Include(r => r.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Donor)
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(m => m.RegistrationID == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        
        public IActionResult Create()
        {
            ViewData["DonorName"] = new SelectList(_context.Donors, "DonorID", "DonorName");
            ViewData["PatientName"] = new SelectList(_context.Patients, "PatientID", "PatientName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationID,DonorID,Status,PatientID,IsActive")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonorName"] = new SelectList(_context.Donors, "DonorID", "DonorName", registration.DonorID);
            ViewData["PatientName"] = new SelectList(_context.Patients, "PatientID", "PatientName", registration.PatientID);
            return View(registration);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            ViewData["DonorName"] = new SelectList(_context.Donors, "DonorID", "DonorName", registration.DonorID);
            ViewData["PatientName"] = new SelectList(_context.Patients, "PatientID", "PatientName", registration.PatientID);
            return View(registration);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationID,DonorID,Status,PatientID,IsActive")] Registration registration)
        {
            if (id != registration.RegistrationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.RegistrationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "CellPhoneNo", registration.DonorID);
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "PatientID", registration.PatientID);
            return View(registration);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Donor)
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(m => m.RegistrationID == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationID == id);
        }
    }
}
