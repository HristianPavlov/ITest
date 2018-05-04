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

            //Trying to fix the keys


            //builder.Entity<UserTests>()
            //    .HasKey(x => new { x.UserId, x.TestId });

            //builder.Entity<UserTests>()
            //    .HasOne(userTests => userTests.User)
            //    .WithMany(User => User.Tests)
            //    .HasForeignKey(u => u.UserId);

            //builder.Entity<UserTests>()
            //  .HasOne(t => t.Test)
            //  .WithMany(u => u.Users)
            //  .HasForeignKey(t => t.TestId);


            //builder.Entity<UserTestAnswers>()
            //   .HasKey(x => new { x.UserTestsId, x.AnswerId });

            //builder.Entity<UserTestAnswers>()
            //    .HasOne(uta => uta.UserTest)
            //    .WithMany(userTest => userTest.Answers)
            //    .HasForeignKey(uta => uta.UserTestsId);

            //builder.Entity<UserTestAnswers>()
            //  .HasOne(uta => uta.Answer)
            //  .WithMany(answer => answer.UserTests)
            //  .HasForeignKey(uta => uta.AnswerId);

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
