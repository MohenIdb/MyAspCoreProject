using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MyAspCoreFinalProject.Data;
using MyAspCoreFinalProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace MyAspCoreFinalProject.Controllers
{
    public class DonorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public DonorController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Donors.Include(d=>d.BloodGroup);
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            Donor donor = new Donor();
            ViewData["BloodGroupName"] = new SelectList(_context.BloodGroups, "BloodGroupID", "BloodGroupName");
            return View(donor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donor donor)
        {
            if (ModelState.IsValid)
            {
                string myFileName = GetUploadedFileName(donor);
                donor.DonorImage = myFileName;

                _context.Add(donor);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            ViewData["BloodGroupID"] = new SelectList(_context.BloodGroups, "BloodGroupID", "BloodGroupName");
            return RedirectToAction("Index");
        }


        private string GetUploadedFileName(Donor donor)
        {
            string myFileName = null;

            if (donor.UploadImage != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Images/");
                myFileName = Guid.NewGuid().ToString() + "_" + donor.UploadImage.FileName;
                string filePath = Path.Combine(uploadsFolder, myFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    donor.UploadImage.CopyTo(fileStream);
                }
            }
            return myFileName;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Donor donor = await _context.Donors.FindAsync(id);
            ViewData["BloodGroupName"] = new SelectList(_context.BloodGroups, "BloodGroupID", "BloodGroupName");
            return View(donor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Donor donor)
        {
            if (ModelState.IsValid)
            {
                if (donor.UploadImage != null)
                {
                    string uniqueFileName = GetUploadedFileName(donor);
                    donor.DonorImage = uniqueFileName;
                }
                _context.Entry(donor).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            //ViewData["BloodGroupName"] = new SelectList(_context.BloodGroups, "BloodGroupID", "BloodGroupName");
            //return View(customer);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donor = await _context.Donors
                .Include(d=>d.BloodGroup)
                .FirstOrDefaultAsync(m => m.DonorID == id);
            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Donors.FindAsync(id);
            _context.Donors.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donor = await _context.Donors
                .Include(d=>d.BloodGroup)
                .FirstOrDefaultAsync(m => m.DonorID == id);
            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        private bool DonorExists(int id)
        {
            return _context.Donors.Any(e => e.DonorID == id);
        }

    }
}
