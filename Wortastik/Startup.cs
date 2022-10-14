using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wortastik.Data;

namespace Worktastik
{
    public class Startup
    {
        /// <summary>Initializes a new instance of the <see cref="T:Worktastik.Startup" /> class.</summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>Gets the configuration.</summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>Configures the services.</summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>Configures the specified application.</summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            CreateRole(serviceProvider, "Admin").Wait();
            CreateDefaultUser(serviceProvider, "Admin", "admin@worktastic.com", "Test1.").Wait();
        }

        /// <summary>Creates the role.</summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="roleName">Name of the role.</param>
        public async Task CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            var roleExits = roleManager != null && await roleManager.RoleExistsAsync(roleName);

            if (roleExits)
                return;
            if (roleManager != null) await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        /// <summary>Creates the default user.</summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="username">The username.</param>
        /// <param name="pw">The pw.</param>
        public async Task CreateDefaultUser(IServiceProvider serviceProvider, string roleName, string username,
            string pw)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            if (userManager != null)
            {
                var user = await userManager.FindByNameAsync(username);

                if (user == null)
                {
                    var newUser = new IdentityUser()
                    {
                        UserName = username,
                        Email = username
                    };
                    await userManager.CreateAsync(newUser, pw);
                }

                await userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}
