using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ITest.Data.Models;

namespace ITest.Data
{
    public class ITestDbContext : IdentityDbContext<User>
    {
        public ITestDbContext(DbContextOptions<ITestDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<UserTests>()
                .HasKey(x => new { x.UserId, x.TestId });

            builder.Entity<UserTests>()
                .HasOne(u => u.User)
                .WithMany(t => t.Tests)
                .HasForeignKey(u => u.UserId);


            builder.Entity<UserTests>()
              .HasOne(t => t.Test)
              .WithMany(u => u.Users)
              .HasForeignKey(t => t.TestId);






            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
