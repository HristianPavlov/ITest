using ITest.Data.Models;
using ITest.Data.Models.Enums;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.Infrastructure.CustomExceptions;
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
            var allTests = tests.All.AsNoTracking().Include(t=>t.Category).AsNoTracking();
                

            return mapper.ProjectTo<TestEditDTO>(allTests);
        }
        public int GetTestCountDownByTestId(Guid id)

        {
            var testsFromThisCategory = tests.All.AsNoTracking().Where(test => test.Id == id);
            var currentTest = testsFromThisCategory.First();
            var countDownMins = currentTest.TimeInMinutes;
            return countDownMins;
        }
        public TestDTO GetRandomTestFromCategory(Guid categoryID)
        {
            // test status should be published and test shouldnt be deleted
             var testsFromThisCategory = tests.All.Where(test => test.CategoryId == categoryID && test.Status == TestStatus.Published && !test.IsDeleted).
                                                        Include(t => t.Questions).
                                                        ThenInclude(x => x.Answers)
                                                        .ToList();
            if (testsFromThisCategory.Count() < 1)
            {
                throw new CategoryEmptyException();
            }
            var randomTest = testsFromThisCategory[this.random.GiveMeRandomNumber(testsFromThisCategory.Count())];
            var randomTestDto = mapper.MapTo<TestDTO>(randomTest);
            return randomTestDto;
        }
        public TestDTO GetTestById(Guid id)
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
