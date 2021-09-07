using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeerPlatform_Team2.Controllers;
using LeerPlatform_Team2.Models;
using LeerPlatform_Team2.Services;
using LeerPlatform_Team2.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LeerPlatform_Team2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<TblGebruiker, IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<GIPContext>();

            services.AddMvc();
            services.AddControllersWithViews()  
                .AddRazorRuntimeCompilation();
            services.AddDbContext<GIPContext>(ServiceLifetime.Scoped);

            services.AddControllersWithViews().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddScoped<IFunctionaliteitenService, FunctionaliteitenService>();
            services.AddScoped<INiewsService, NiewsService>();
            services.AddScoped<ILessenService, LessenService>();
            services.AddScoped<IPlanningService, PlanningService>();
            services.AddScoped<ILokalenService, LokalenService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IInschrijvingPlanService, InschrijvingPlanService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IInschrijvingService, InschrijvingService>();
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

                CreateRoles(serviceProvider).Wait();
}

        readonly string[] ROLES = new string[] { "Admin", "Docent", "Student", "Editor" };
        // Gebaseerd op https://dotnetdetail.net/role-based-authorization-in-asp-net-core-3-0/
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<TblGebruiker>>();

            IdentityResult roleResult;
            //here in this line we are adding the Roles
            foreach (string role in ROLES)
            {
                var roleCheck = await RoleManager.RoleExistsAsync(role);
                if (!roleCheck)
                {
                    //here in this line we are creating admin role and seed it to the database
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //here we are assigning the Admin role to the User that we have registered above 
            //Now, we are assinging admin role to this user("Ali@gmail.com"). When will we run this project then it will
            //be assigned to that user.
            TblGebruiker user = await UserManager.FindByEmailAsync("adw-admin@ucll.be");
            if (user != null)
            {
                foreach (string role in ROLES)
                {
                    await UserManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
