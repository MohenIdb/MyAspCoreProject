using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyAspCoreFinalProject.Data;
using MyAspCoreFinalProject.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace MyAspCoreFinalProject.Controllers
{
    public class MultipleController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHost;




        public MultipleController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;

        }
        public IActionResult Index()
        {
            List<BloodDonor> bloodDonors;
            bloodDonors = _context.BloodDonors.ToList();
            return View(bloodDonors);
        }

        [HttpGet]

        public IActionResult Create()
        {
            BloodDonor bloodDonor = new BloodDonor();
            bloodDonor.DonorDetails.Add(new DonorDetails() { DonorDetailsId = 1 });
            return View(bloodDonor);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BloodDonor bloodDonor)
        {
            bloodDonor.DonorDetails.RemoveAll(n => n.DonateTime == 0);


            if (ModelState.IsValid)
            {
                string uniqueFileName = GetUploadedFileName(bloodDonor);
                bloodDonor.PhotoUrl = uniqueFileName;

                _context.Add(bloodDonor);
                await _context.SaveChangesAsync();
                //return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }


        private string GetUploadedFileName(BloodDonor bloodDonor)
        {
            string uniqueFileName = null;

            if (bloodDonor.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Images/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + bloodDonor.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    bloodDonor.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult Details(int Id)
        {
            BloodDonor bloodDonor = _context.BloodDonors.Include(e => e.DonorDetails)
                .Where(a => a.Id == Id).FirstOrDefault();
            return View(bloodDonor);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            BloodDonor bloodDonor = _context.BloodDonors.Include(e => e.DonorDetails)
                .Where(a => a.Id == Id).FirstOrDefault();
            return View(bloodDonor);
        }

        [HttpPost]

        public IActionResult Delete(BloodDonor bloodDonor)
        {
            _context.Attach(bloodDonor);
            _context.Entry(bloodDonor).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.BloodDonors.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            return View(applicant);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BloodDonor bloodDonor, int id)
        {
            if (id != bloodDonor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(bloodDonor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bloodDonor);
        }
    }
}
