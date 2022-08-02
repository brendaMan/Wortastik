using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Worktastic.Models;

namespace Wortastik.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<JobPosting> JobPosting { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
