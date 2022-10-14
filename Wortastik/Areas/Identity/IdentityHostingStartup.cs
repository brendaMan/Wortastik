using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wortastik.Data;

[assembly: HostingStartup(typeof(Wortastik.Areas.Identity.IdentityHostingStartup))]
namespace Wortastik.Areas.Identity
{
    /// <summary>Class IdentityHostingStartup.
    /// Implements the <see cref="IHostingStartup" /></summary>
    public class IdentityHostingStartup : IHostingStartup
    {
        /// <summary>Configure the <see cref="T:Microsoft.AspNetCore.Hosting.IWebHostBuilder">IWebHostBuilder</see>.</summary>
        /// <param name="builder">The builder.</param>
        /// <remarks>Configure is intended to be called before user code, allowing a user to overwrite any changes made.</remarks>
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}