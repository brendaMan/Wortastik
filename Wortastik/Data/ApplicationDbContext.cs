using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Wortastik.Models;

namespace Wortastik.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>Gets or sets the job postings.</summary>
        /// <value>The job postings.</value>
        public DbSet<JobPosting> JobPostings { get; set; }
        /// <summary>Initializes a new instance of the <see cref="T:Wortastik.Data.ApplicationDbContext" /> class.</summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
