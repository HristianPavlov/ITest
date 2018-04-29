﻿using ITest.Data.Models;
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
        private readonly ISaver saver;

        public TestService(IMappingProvider mapper,
                                 IRepository<Test> tests,
                                 ISaver saver)
        {
            this.mapper = mapper;
            this.tests = tests;
            this.saver = saver;
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
            //var random = new Random();
            //search if it's possible to get random from collection*ElementAt not working correctly*
            var testsFromThisCategory = tests.All.Where(test => test.CategoryId == categoryID).
                                                        Include(t => t.Questions).
                                                        ThenInclude(x => x.Answers);
                                                        //.ToList();
            if (testsFromThisCategory.Count() < 1)
            {
                throw new ArgumentNullException("Category currently empty");
            }
            //var randomTest = testsFromThisCategory[random.Next(testsFromThisCategory.Count())];
            var randomTest = testsFromThisCategory.FirstOrDefault();
            var randomTestDto = mapper.MapTo<TestDTO>(randomTest);
            return randomTestDto;
        }
        public TestDTO GetTestById(int id)
        {
            var testsFromThisCategory = tests.All.Where(test => test.Id == id).
                                                        Include(t => t.Questions).
                                                        ThenInclude(x => x.Answers);
            var currentTest = testsFromThisCategory.First();
            var foundTestDto = mapper.MapTo<TestDTO>(currentTest);
            return foundTestDto;
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
