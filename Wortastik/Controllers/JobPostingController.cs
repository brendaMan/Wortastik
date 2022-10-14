using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wortastik.Data;
using Wortastik.Models;

namespace Worktastic.Controllers
{
    /// <summary>Class JobPostingController.
    /// Implements the <see cref="Controller" /></summary>
    [Authorize]
    public class JobPostingController : Controller
    {
        /// <summary>The context</summary>
        private readonly ApplicationDbContext _context;

        /// <summary>Initializes a new instance of the <see cref="T:Worktastic.Controllers.JobPostingController" /> class.</summary>
        /// <param name="context">The context.</param>
        public JobPostingController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns>IActionResult.</returns>
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var allPostings = _context.JobPostings.ToList();
                return View(allPostings);
            }

            var jobPostingsFromDb = _context.JobPostings.Where(x => x.OwnerUsername == User.Identity.Name).ToList();

            return View(jobPostingsFromDb);
        }

        /// <summary>Creates the edit job posting.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        public IActionResult CreateEditJobPosting(int id)
        {
            if (id != 0)
            {
                var jobPostingFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == id);

                if ((jobPostingFromDb.OwnerUsername != User.Identity.Name) && !User.IsInRole("Admin"))
                {
                    return Unauthorized();
                }

                if (jobPostingFromDb != null)
                {
                    return View(jobPostingFromDb);
                }
                else
                {
                    return NotFound();
                }
            }

            return View();
        }

        /// <summary>Creates the edit job.</summary>
        /// <param name="jobPosting">The job posting.</param>
        /// <param name="file">The file.</param>
        /// <returns>IActionResult.</returns>
        public IActionResult CreateEditJob(JobPosting jobPosting, IFormFile file)
        {
            jobPosting.OwnerUsername = User.Identity.Name;

            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var bytes = ms.ToArray();
                    jobPosting.CompanyImage = bytes;
                }
            }

            if (jobPosting.Id == 0)
            {
                // Add new job if not editing
                _context.JobPostings.Add(jobPosting);
            }
            else
            {
                var jobFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == jobPosting.Id);

                if (jobFromDb == null)
                {
                    return NotFound();
                }

                jobFromDb.CompanyImage = jobPosting.CompanyImage;
                jobFromDb.CompanyName = jobPosting.CompanyName;
                jobFromDb.ContactMail = jobPosting.ContactMail;
                jobFromDb.ContactPhone = jobPosting.ContactPhone;
                jobFromDb.ContactWebsite = jobPosting.ContactWebsite;
                jobFromDb.Description = jobPosting.Description;
                jobFromDb.JobLocation = jobPosting.JobLocation;
                jobFromDb.JobTitle = jobPosting.JobTitle;
                jobFromDb.Salary = jobPosting.Salary;
                jobFromDb.StartDate = jobPosting.StartDate;
                // jobFromDb.OwnerUsername = jobPosting.OwnerUsername;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        /// <summary>Deletes the job posting by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public IActionResult DeleteJobPostingById(int id)
        {
            if (id == 0)
                return BadRequest();

            var jobPostingFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == id);

            if (jobPostingFromDb == null)
                return NotFound();

            _context.JobPostings.Remove(jobPostingFromDb);
            _context.SaveChanges();

            return Ok();
        }

    }
}