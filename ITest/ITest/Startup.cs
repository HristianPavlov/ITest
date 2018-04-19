using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ITest.Data;
using ITest.Models;
using ITest.Services.External;
using ITest.Data.Models;
using ITest.Services.Data.Contracts;
using ITest.Services.Data;
using ITest.Infrastructure.Providers;
using AutoMapper;
using ITest.Data.UnitOfWork;
using ITest.Data.Repository;
using AutoMapper;
using ITest.Infrastructure.Providers;
using ITest.Services.Data;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.Infrastructure.RoleInitializer;

namespace ITest
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
            services.AddDbContext<ITestDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ITestDbContext>()
                .AddDefaultTokenProviders();


            services.AddMvc();

            services.AddTransient<ICategoriesService, CategoriesService>();

            services.AddAutoMapper();

            services.AddScoped<IMappingProvider, MappingProvider>();

            // Add application services.

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ISaver, EFSaver>();

            // services.AddMvc();
         
            services.AddTransient<IQuestionService, QuestionService>();

            services.AddAutoMapper();

            services.AddScoped<IMappingProvider, MappingProvider>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            UserRoleInitializer.SeedRoles(roleManager);
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
