using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wortastik.Data;
using Wortastik.Models;

namespace Wortastik.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>The context</summary>
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        /// <summary>Initializes a new instance of the <see cref="T:Wortastik.Controllers.HomeController" /> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns>IActionResult.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>Privacies this instance.</summary>
        /// <returns>IActionResult.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>Errors this instance.</summary>
        /// <returns>IActionResult.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>Gets the job posting.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult GetJobPosting(int id)
        {
            if (id == 0)
                return BadRequest();

            var jobPostingFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == id);

            if (jobPostingFromDb == null)
                return NotFound();

            return Ok(jobPostingFromDb);
        }

        /// <summary>Gets the job postings partial.</summary>
        /// <param name="query">The query.</param>
        /// <returns>IActionResult.</returns>
        public IActionResult GetJobPostingsPartial(string query)
        {
            List<JobPosting> jobPostings;

            if (string.IsNullOrWhiteSpace(query))
                jobPostings = _context.JobPostings.ToList();
            else
                jobPostings = _context.JobPostings
                    .Where(x => x.JobTitle.ToLower().Contains(query.ToLower()))
                    .ToList();

            return PartialView($"_JobPostingListParcial", jobPostings);
        }
    }
}