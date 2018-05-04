using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using ITest.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Services.Data
{
    public class QuestionService : IQuestionService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Question> questions;
        private readonly IRepository<Answer> answers;

        public QuestionService(ISaver saver, IMappingProvider mapper, IRepository<Answer> answers, IRepository<Question> questions)
        {
            this.saver = saver;
            this.mapper = mapper;
            this.answers = answers;
            this.questions = questions;
        }

        public void Create(TestDTO dto)
        {
            var model = this.mapper.MapTo<Test>(dto);
            //this.questions.Add(model);
            this.saver.SaveChanges();
        }
    }
}
