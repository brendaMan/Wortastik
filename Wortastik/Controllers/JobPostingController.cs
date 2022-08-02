using Microsoft.AspNetCore.Mvc;
using Worktastic.Models;

namespace Wortastik.Controllers
{
    public class JobPostingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateEditJobPosting(int id)
        {
            return View();
        }
        public IActionResult CreateEditJob(JobPosting jobPosting)
        {
            // TODO
            return RedirectToAction("Index");
        }
    }
}
