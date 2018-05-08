using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ITest.Data.Models;
using ITest.Data.Models.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace ITest.Data
{
    public class ITestDbContext : IdentityDbContext<User>
    {
        public ITestDbContext(DbContextOptions<ITestDbContext> options)
            : base(options)
        {
         // this.Seed().Wait();
        }
        public ITestDbContext()
        {

        }
        public DbSet<Category> Categories { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           

            //Trying to fix the keys
            builder.Entity<Test>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tests)
                .HasForeignKey(t => t.CategoryId);
            //strin + int take care!!!!!
            builder
                .Entity<Test>()
                .HasIndex(u => u.Name)
                .IsUnique();


            builder.Entity<Question>()
                .HasOne(q => q.Test)
                .WithMany(t => t.Questions)
                .HasForeignKey(q => q.TestId);

            builder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);

            builder.Entity<UserTestAnswers>()
                .HasOne(uta => uta.UserTest)
                .WithMany(ut => ut.Answers)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        private async Task Seed()
        {
            this.Database.EnsureCreated();

            if (!this.Roles.Any(r => r.Name == "Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                this.Roles.Add(adminRole);
            }
            if (!this.Roles.Any(r => r.Name == "User"))
            {
                var userRole = new IdentityRole("User");
                this.Roles.Add(userRole);
            }

            if (!this.Categories.Any())
            {
                var categoriesToAdd = new List<Category>()
            {
                new Category()
                {
                    Name = "Java"
                },

                new Category()
                {
                    Name = ".NET"
                },

                new Category()
                {
                    Name = "Javascript"
                },

                new Category()
                {
                    Name = "SQL"
                }
            };

                categoriesToAdd
                    .ForEach(c => this.Categories.Add(c));

            }

            await this.SaveChangesAsync();
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
