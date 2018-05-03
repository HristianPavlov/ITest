
using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ITest.Services.Data
{
    public class CreateTestService : ICreateTestService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Question> questions;
        private readonly IRepository<Test> tests;
        private readonly IRepository<Answer> answers;
        
     


        public CreateTestService(ISaver saver, IMappingProvider mapper, IRepository<Answer> answers, IRepository<Question> questions, IRepository<Test> tests)
        {
            this.saver = saver;
            this.mapper = mapper;
            this.answers = answers;
            this.questions = questions;
            this.tests = tests;
        }

        public void Create(TestDTO dto)
        {
            var model = this.mapper.MapTo<Test>(dto);
            this.tests.Add(model);
            this.saver.SaveChanges();
        }

        public void Update(TestEditDTO dto) 
        {


            var TestUpdate = this.tests.All.Include(x=> x.Questions)
                .ThenInclude(q=>q.Answers)
                .FirstOrDefault(x => x.Name == dto.Name);
                //.Include(t=>t.Category)
                //.Include(t => t.Questions)
                //    .ThenInclude(q=>q.Answers)
              //  ;/*.AsNoTracking()*/

           var id = TestUpdate.Id;
            //var cartegoryID = TestUpdate.CategoryId;
            //var cat= TestUpdate.Category;

            TestUpdate=    this.mapper.MapTo(dto,TestUpdate);
            TestUpdate.Id = id;
            //TestUpdate.Id = id;
            //TestUpdate.CategoryId = cartegoryID;
            //  TestUpdate.Category = cat;
            //TestUpdate.Category.Id = cat.Id;
            //TestUpdate.Category.Name = cat.Name;
            //TestUpdate.Category.Tests = cat.Tests;




            // var qqq = this.mapper.MapTo<Test>(dto);
            //TestUpdate.TimeInMinutes = dto.TimeInMinutes;
            //TestUpdate.Status = dto.Status;

            //TestUpdate.Questions = dto.Questions;


            this.tests.Update(TestUpdate);
            this.saver.SaveChanges();
        }
    }
}

