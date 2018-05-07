
using ITest.Data.Models;
using ITest.Data.Models.Enums;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

            var TestUpdate = this.tests.All.Where(t => t.Name == dto.Name)
               .First();


            //var id = TestUpdate.Id;
            //dto.Id = id.ToString();
            //var catId = TestUpdate.CategoryId;
            //dto.CategoryId = catId.ToString();

            //this.mapper.MapTo(dto, TestUpdate);

            //TestUpdate.Id = id;
            //TestUpdate.CategoryId = catId;
            TestUpdate.TimeInMinutes = dto.TimeInMinutes;
            TestUpdate.Name = dto.Name;
            TestUpdate.Status = dto.Status;



            this.tests.Update(TestUpdate);

            foreach (var item in dto.Questions)
            {

                if (item.Id!=null)
                {
                    var x = this.mapper.MapTo<Question>(item);
                    this.UpdateQuestions(x);
                }
                else
                {
                    var x = this.mapper.MapTo<Question>(item);
                    x.TestId = TestUpdate.Id;
                    this.questions.Add(x);

                }
               
                
                
            }
            this.saver.SaveChanges();


            //foreach (var item in dto.Questions)
            //{
            //    foreach (var A in item.Answers)
            //    {
            //        var x = this.mapper.MapTo<Answer>(A);

            //        this.UpdateAnswers(x);
            //    }
                
            //}
            //this.saver.SaveChanges();
        }

        private void UpdateQuestions(Question item)
        {
            this.questions.Update(item);
            
        }
        //private void UpdateAnswers(Answer item)
        //{
        //    this.answers.Update(item);

        //}

        public void PublishedUpdate(TestEditDTO dto)
        {
            var listOfAnswers = new List<AnswerEditDTO>();
            foreach (var item in dto.Questions)
            {
                foreach (var A in item.Answers)
                {

                    var x = this.mapper.MapTo<Answer>(A);
                    this.answers.Update(x);
                    //listOfAnswers.Add(A);
                }
            }

            // var Answers = this.mapper.EnumProjectTo<Answer>(listOfAnswers);



            this.saver.SaveChanges();

        }

        public void DeleteTest(string name)
        {
            var TestUpdate = this.tests.All.Where(t => t.Name == name)
                .Include(t => t.Category)
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .First();


            TestUpdate.Status = (TestStatus)Enum.Parse(typeof(TestStatus), "Deleted");



            foreach (var item in TestUpdate.Questions)
            {
                this.DeleteQuestion(item);
            }

            this.tests.Delete(TestUpdate);

            this.saver.SaveChanges();

        }

        public void DeleteQuestion(Question q)
        {
            foreach (var item in q.Answers)
            {
                this.answers.Delete(item);
            }

            this.questions.Delete(q);


        }

        public void Disable(string name)
        {
            var TestUpdate = this.tests.All.Where(t => t.Name == name)
                             .First();


            TestUpdate.Status = (TestStatus)Enum.Parse(typeof(TestStatus), "Disabled");

            this.tests.Update(TestUpdate);
            this.saver.SaveChanges();

        }

        public void Publish(string name)
        {
            var TestUpdate = this.tests.All.Where(t => t.Name == name)
                             .First();


            TestUpdate.Status = (TestStatus)Enum.Parse(typeof(TestStatus), "Published");

            this.tests.Update(TestUpdate);
            this.saver.SaveChanges();

        }

    }
}

