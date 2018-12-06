using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIBA_WEB.Models;
using SIBA_WEB.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SIBA_WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<IdentityOptions>(options => {
                options.User.RequireUniqueEmail = true;
            });
            var accountName = Configuration.GetValue<String>("Conexiones:AccountName");
            var accountKey = Configuration.GetValue<String>("Conexiones:AccountKey");
            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("SIBA_DATA"));
            services.AddTransient<IRepository>(w => {
                return new Repository(
                    accountName,
                    accountKey
                    //UserManager<IdentityUser> userManager,
                    //RoleManager<IdentityRole> roleManager,
                    );
            });
            //services.AddSingleton<IBloobStorage> (s => { })
            services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlite(
                    //options.UseSqlServer(
                        Configuration.GetConnectionString("EntityContextConnection")));

            services
                //.AddDefaultIdentity<IdentityUser>()
                .AddIdentity<IdentityUser,IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<IdentityContext>();
                

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            //app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseAuthentication();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            IdentityStuff(userManager, roleManager);
        }


        public void IdentityStuff(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager )
        {

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var adminRole = new IdentityRole("Admin");
                var res = roleManager.CreateAsync(adminRole).Result;
            }
            if (!roleManager.RoleExistsAsync("Alumno").Result)
            {
                var adminRole = new IdentityRole("Alumno");
                var res = roleManager.CreateAsync(adminRole).Result;
            }

            if (userManager.FindByEmailAsync("admin2@admin.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin2@admin.com",
                    Email = "admin2@admin.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Password123@!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
