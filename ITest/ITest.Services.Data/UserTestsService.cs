using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITest.Services.Data
{
    public class UserTestsService : IUserTestsService
    {
        private readonly IMappingProvider mapper;
        private readonly ITestService testService;
        private readonly IRepository<UserTests> userTests;
        private readonly ISaver saver;

        public UserTestsService(IMappingProvider mapper,
                                ITestService testService,
                                IRepository<UserTests> userTests,
                                ISaver saver)
        {
            this.mapper = mapper;
            this.testService = testService;
            this.userTests = userTests;
            this.saver = saver;
        }
        public bool UserStartedTest(string userId, string category)
        {
            if (userTests.All.Any(x => x.UserId == userId && x.Category == category))
            {
                return true;
            }
            return false;
        }
        public DateTime? StartedTestCreationTime(string userId, string category)
        {
            return userTests.All.First(x => x.UserId == userId && x.Category == category).CreatedOn;
        }
        public bool TestIsSubmitted(string userId, string category)
        {
            return userTests.All.First(x => x.UserId == userId && x.Category == category).Submitted;
        }
        public int GetTestIdFromUserIdAndCategory(string userId, string category)
        {
            var testsFromThisCategory = userTests.All.First(x => x.UserId == userId && x.Category == category);
            return testsFromThisCategory.TestId;
        }
        public IEnumerable<UserTestsDTO> GetAllUserTests()
        {
            var allUserTests = userTests.All.
                                        Include(t => t.Test).
                                        Include(u => u.User);
            return mapper.ProjectTo<UserTestsDTO>(allUserTests);
        }
        public void SaveTest(UserTestsDTO test)
        {
            var testModel = mapper.MapTo<UserTests>(test);
            testModel.CreatedOn = DateTime.Now;
            userTests.Add(testModel);
            saver.SaveChanges();
        }
        public void Publish(UserTestsDTO test)
        {
            var testModel = mapper.MapTo<UserTests>(test);
            var score = testService.GetResult(test);

            //testModel.Score = score;
            //userTests.Update(testModel);
            //saver.SaveChanges();
            //Update doesnt work "cannot track many instances of same type"

            var updatingtest = userTests.All.FirstOrDefault(x => x.TestId == test.TestId && x.UserId == test.UserId);
            updatingtest.ExecutionTime = testModel.ExecutionTime;
            updatingtest.Score = score;
            updatingtest.SerializedAnswers = testModel.SerializedAnswers;
            updatingtest.Submitted = true;
            saver.SaveChanges();
        }

    }
}
