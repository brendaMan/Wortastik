using System;

namespace Wortastik.Models
{
    /// <summary>Class JobPosting.</summary>
    public class JobPosting
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the job title.</summary>
        /// <value>The job title.</value>
        public string JobTitle { get; set; }

        /// <summary>Gets or sets the job location.</summary>
        /// <value>The job location.</value>
        public string JobLocation { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>Gets or sets the salary.</summary>
        /// <value>The salary.</value>
        public int Salary { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        public DateTime StartDate { get; set; }

        /// <summary>Gets or sets the name of the company.</summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }

        /// <summary>Gets or sets the contact phone.</summary>
        /// <value>The contact phone.</value>
        public string ContactPhone { get; set; }

        /// <summary>Gets or sets the contact mail.</summary>
        /// <value>The contact mail.</value>
        public string ContactMail { get; set; }

        /// <summary>Gets or sets the contact website.</summary>
        /// <value>The contact website.</value>
        public string ContactWebsite { get; set; }

        /// <summary>Gets or sets the company image.</summary>
        /// <value>The company image.</value>
        public byte[] CompanyImage { get; set; }

        /// <summary>Gets or sets the owner username.</summary>
        /// <value>The owner username.</value>
        public string OwnerUsername { get; set; }
    }
}
