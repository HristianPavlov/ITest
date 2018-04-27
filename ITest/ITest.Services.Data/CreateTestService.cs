
using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.Infrastructure.Providers;

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

    }
}

