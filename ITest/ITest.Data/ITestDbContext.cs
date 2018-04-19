using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ITest.Data.Models;
using ITest.Data.Models.Abstracts;

namespace ITest.Data
{
    public class ITestDbContext : IdentityDbContext<User>
    {
        public ITestDbContext(DbContextOptions<ITestDbContext> options)
            : base(options)
        {
        }


        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //strin + int take care!!!!!
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

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
