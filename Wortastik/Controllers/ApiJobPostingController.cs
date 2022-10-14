using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wortastik.Data;
using Wortastik.Filters;
using Wortastik.Models;

namespace Wortastik.Controllers
{
    /// <summary>Class ApiJobPostingController.
    /// Implements the <see cref="ControllerBase" /></summary>
    [Route("api/jobposting")]
    [ApiController]
    [ApiKeyAuthoriziation]
    public class ApiJobPostingController : ControllerBase
    {
        /// <summary>The context</summary>
        private readonly ApplicationDbContext _context;

        /// <summary>Initializes a new instance of the <see cref="T:Wortastik.Controllers.ApiJobPostingController" /> class.</summary>
        /// <param name="context">The context.</param>
        public ApiJobPostingController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>Gets all.</summary>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var allJobPostings = _context.JobPostings.ToArray();

            return Ok(allJobPostings);
        }

        /// <summary>Gets the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetById")]

        public IActionResult GetById(int id)
        {
            var jobPosting = _context.JobPostings.SingleOrDefault(x => x.Id == id);

            if (jobPosting == null)
                return NotFound();

            return Ok(jobPosting);
        }

        /// <summary>Creates the specified job posting.</summary>
        /// <param name="jobPosting">The job posting.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("Create")]
        public IActionResult Create(JobPosting jobPosting)
        {
            if (jobPosting.Id != 0)
                return BadRequest();

            _context.JobPostings.Add(jobPosting);
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>Deletes the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var jobPosting = _context.JobPostings.SingleOrDefault(x => x.Id == id);

            if (jobPosting == null)
                return NotFound();

            _context.JobPostings.Remove(jobPosting);
            _context.SaveChanges();

            return Ok("Deleted object");

        }

        /// <summary>Updates the specified job posting.</summary>
        /// <param name="jobPosting">The job posting.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut("Update")]
        public IActionResult Update(JobPosting jobPosting)
        {
            if (jobPosting.Id == 0)
                return BadRequest("JobPosting does not have id");

            _context.JobPostings.Update(jobPosting);
            _context.SaveChanges();

            return Ok("Object updated");

        }
    }
}
