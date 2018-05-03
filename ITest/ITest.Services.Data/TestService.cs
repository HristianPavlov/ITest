using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ITest.Services.Data
{
    public class TestService : ITestService
    {
        private readonly IMappingProvider mapper;
        private readonly IRepository<Test> tests;
        private readonly IRandomProvider random;
        private readonly ISaver saver;

        public TestService(IMappingProvider mapper,
                                 IRepository<Test> tests,
                                 IRandomProvider random,
                                 ISaver saver)
        {
            this.mapper = mapper;
            this.tests = tests;
            this.random = random;
            this.saver = saver;
        }

        public IEnumerable<TestDTO> GetAllTests()
        {//Why the fuck you called it GetAllTest and add object Category 

            //.Include(test => test.Questions)
            //        .ThenInclude(q => q.Answers)
            var allTests = tests.All.AsNoTracking()
                .Include(test=>test.Category).AsNoTracking();
         
            return mapper.ProjectTo<TestDTO>(allTests);
        }

        public IEnumerable<TestEditDTO> GetAllTestsWithOutStuffInIttEditDTO()
        {
            //.Include(test => test.Questions)
            //        .ThenInclude(q => q.Answers)
            var allTests = tests.All.AsNoTracking();
                

            return mapper.ProjectTo<TestEditDTO>(allTests);
        }

        public int GetTestCountDownByTestId(int id)
        {
            var testsFromThisCategory = tests.All.AsNoTracking().Where(test => test.Id == id);
            var currentTest = testsFromThisCategory.First();
            var countDownMins = currentTest.TimeInMinutes;
            return countDownMins;
        }

        public TestDTO GetRandomTestFromCategory(int categoryID)
        {
            //var random = new Random();
            var testsFromThisCategory = tests.All.AsNoTracking().Where(test => test.CategoryId == categoryID).
                                                        Include(t => t.Questions).
                                                        ThenInclude(x => x.Answers)
                                                        .ToList();
            if (testsFromThisCategory.Count() < 1)
            {
                throw new ArgumentNullException("Category currently empty");
            }
            //var randomTest = testsFromThisCategory[random.Next(testsFromThisCategory.Count())];
            //var randomTest = testsFromThisCategory.FirstOrDefault();
            var randomTest = testsFromThisCategory[this.random.GiveMeRandomNumber(testsFromThisCategory.Count())];
            var randomTestDto = mapper.MapTo<TestDTO>(randomTest);
            return randomTestDto;
        }
        public TestDTO GetTestById(int id)
        {
            var testsFromThisCategory = tests.All.AsNoTracking().Where(test => test.Id == id).
                                                        Include(t => t.Questions).
                                                        ThenInclude(x => x.Answers);
            var currentTest = testsFromThisCategory.First();
            var foundTestDto = mapper.MapTo<TestDTO>(currentTest);
            return foundTestDto;
        }
        public TestDTO GetTestByName(string name)
        {   var testsFromThisCategory = tests.All.AsNoTracking().Where(test => test.Name == name).
                                                        Include(t=>t.Category).
                                                        Include(t => t.Questions).
                                                        ThenInclude(x => x.Answers);
            var currentTest = testsFromThisCategory.First();
            var foundTestDto = mapper.MapTo<TestDTO>(currentTest);
            return foundTestDto;
        }

        public decimal GetResult(UserTestsDTO solvedTest)
        {
            var realTests = tests.All.AsNoTracking().Where(t => t.Id == solvedTest.TestId).
                                                Include(t => t.Questions).
                                                ThenInclude(x => x.Answers);
            var realTest = realTests.First();
            decimal correctAnswers = 0;
            var indexOfAnswer = 0;
            foreach (var question in realTest.Questions)
            {
                var currCorrAnswer = question.Answers.FirstOrDefault(a => a.Correct);
                if (currCorrAnswer.Content == solvedTest.StorageOfAnswers[indexOfAnswer])
                {
                    correctAnswers++;
                }
                indexOfAnswer++;
            }
            decimal score = (correctAnswers / realTest.Questions.Count()) * 100;
            return score;
        }

        public TestEditDTO GetTestByNameEditDTO(string name)
        {
            var testsFromThisCategory = tests.All.AsNoTracking().Where(test => test.Name == name).AsNoTracking().
                                                        Include(t => t.Category).AsNoTracking().
                                                        Include(t => t.Questions).
                                                        ThenInclude(x => x.Answers).AsNoTracking();
            var currentTest = testsFromThisCategory.First();

            var foundTestDto = mapper.MapTo<TestEditDTO>(currentTest);
            return foundTestDto;
        }
    }
}
