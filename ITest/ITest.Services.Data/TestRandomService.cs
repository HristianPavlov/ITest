using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace ITest.Services.Data
{
    public class TestRandomService : ITestRandomService
    {
        private readonly IMappingProvider mapper;
        private readonly IRepository<Test> tests;
        private readonly IRepository<UserTests> userTests;
        private readonly UserManager<User> userManager;
        private readonly ISaver saver;

        public TestRandomService(IMappingProvider mapper, IRepository<Test> tests, IRepository<UserTests> userTests, UserManager<User> userManager, ISaver saver)
        {
            this.mapper = mapper;
            this.tests = tests;
            this.userTests = userTests;
            this.userManager = userManager;
            this.saver = saver;
        }
        public bool UserStartedTest(string userId, string category)
        {
            if (userTests.All.Any(x => x.UserId==userId && x.Category==category))
            {
                return true;
            }
            return false;
        }
        public DateTime? StartedTestCreationTime(string userId, string category)
        {
            return userTests.All.First(x => x.UserId == userId && x.Category == category).CreatedOn;
        }
        public int GetTestCountDownByTestId(int id)
        {
            var testsFromThisCategory = tests.All.Where(test => test.Id == id);
            var currentTest = testsFromThisCategory.First();
            var countDownMins = currentTest.TimeInMinutes;
            return countDownMins;
        }

        public TestDTO GetRandomTestFromCategory(int categoryID)
        {
            //must fix it to give random test (the testid must be some random number < tests.count)
            var testsFromThisCategory = tests.All.Where(test => test.CategoryId == categoryID).
                                                        Include(t => t.Questions).ThenInclude(x => x.Answers);
            var randomTest = testsFromThisCategory.First();
            var randomTestDto = mapper.MapTo<TestDTO>(randomTest);
            return randomTestDto;
        }
        public int GetTestIdFromUserIdAndCategory(string userId, string category)
        {
            var testsFromThisCategory = userTests.All.First(x => x.UserId == userId && x.Category == category);
            return testsFromThisCategory.TestId;
        }
        public TestDTO GetTestById(int id)
        {
            var testsFromThisCategory = tests.All.Where(test => test.Id == id).
                                                        Include(t => t.Questions).ThenInclude(x => x.Answers);
            var currentTest = testsFromThisCategory.First();
            var foundTestDto = mapper.MapTo<TestDTO>(currentTest);
            return foundTestDto;
        }
        public void Publish(UserTestsDTO test)
        {
            var testModel = mapper.MapTo<UserTests>(test);
            var score = GetResult(test);
            testModel.Score = score;
            userTests.Update(testModel);
            saver.SaveChanges();
        }
        public void SaveTest(UserTestsDTO test)
        {
            var testModel = mapper.MapTo<UserTests>(test);
            testModel.CreatedOn = DateTime.Now;
            userTests.Add(testModel);
            saver.SaveChanges();
        }

        public decimal GetResult(UserTestsDTO solvedTest)
        {
            var realTests = tests.All.Where(t => t.Id == solvedTest.TestId).
                                                Include(t => t.Questions).ThenInclude(x => x.Answers);
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

        
    }
}
