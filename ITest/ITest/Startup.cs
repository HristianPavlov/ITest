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
using ITest.Infrastructure;
using ITest.Services.Data;
using ITest.Services.Data.Contracts;
using ITest.Infrastructure.RoleInitializer;
using ITest.Infrastructure.Providers;
using ITest.Data.Models;

using ITest.Data.UnitOfWork;
using AutoMapper;
using ITest.Data.Repository;
using ITest.Services.External;
using ITest.Data.Providers;

namespace ITest
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.RegisterData(services);
            this.RegisterAuthentication(services);
            this.RegisterServices(services);
            this.RegisterInfrastructure(services);

        }
        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IUserTestsService, UserTestsService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<ICreateTestService, CreateTestService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailSender, EmailSender>();
        }

        private void RegisterInfrastructure(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();

            services.AddScoped<IMappingProvider, MappingProvider>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IRandomProvider, RandomProvider>();
            services.AddTransient<IRepoTimeProvider, RepoTimeProvider>();
        }
        private void RegisterAuthentication(IServiceCollection services)
        {

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ITestDbContext>()
               .AddDefaultTokenProviders();

            if (this.Environment.IsDevelopment())
            {
                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 0;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
                    options.Lockout.MaxFailedAccessAttempts = 999;
                });
            }
        }


        private void RegisterData(IServiceCollection services)
        {
          
            services.AddDbContext<ITestDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ISaver, EFSaver>();
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
            //The seeding of roles
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
