
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
            var TestUpdate = this.tests.All.Include(x => x.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault(x => x.Name == dto.Name);
          
            var id = TestUpdate.Id;
            TestUpdate = this.mapper.MapTo(dto, TestUpdate);
            TestUpdate.Id = id;

            this.tests.Update(TestUpdate);
            this.saver.SaveChanges();
        }
    }
}

