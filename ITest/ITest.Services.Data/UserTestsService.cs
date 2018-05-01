using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.DTO.Enums;
using ITest.Infrastructure.CustomExceptions;
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
        private readonly IDateTimeProvider dateTime;
        private readonly ITestService testService;
        private readonly ICategoriesService categoriesService;
        private readonly IRepository<UserTests> userTests;
        private readonly IGenericShuffler shuffler;
        private readonly ISaver saver;

        public UserTestsService(IMappingProvider mapper,
                                IDateTimeProvider dateTime,
                                ITestService testService,
                                ICategoriesService categoriesService,
                                IRepository<UserTests> userTests,
                                IGenericShuffler shuffler,
                                ISaver saver)
        {
            this.mapper = mapper;
            this.dateTime = dateTime;
            this.testService = testService;
            this.categoriesService = categoriesService;
            this.userTests = userTests;
            this.shuffler = shuffler;
            this.saver = saver;
        }

        public bool UserStartedTest(string userId, string category)
        {
            if (this.userTests.All.Any(x => x.UserId == userId && x.Category == category))
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
        public IEnumerable<UserTestsDTO> GetCurrentUserTests(string userId)
        {
            var currentUserTests = userTests.All.Where(u => u.UserId == userId).
                                        Include(t => t.Test).
                                        Include(u => u.User);
            var currentUserCategories = mapper.ProjectTo<UserTestsDTO>(currentUserTests).ToList();
            if (currentUserCategories.Count() == 0)
            {
                return currentUserCategories;
            }
            foreach (var item in currentUserCategories)
            {
                if (item.Submitted)
                {
                    item.CategoryState = UserTestState.Submitted;
                }
                else
                {
                    if ((item.CreatedOn.Value.AddMinutes(item.Test.TimeInMinutes) - dateTime.GetDateTimeNow()).TotalSeconds < 0)
                    {
                        item.CategoryState = UserTestState.NotSubmittedOnTime;
                    }
                    else
                    {
                        item.CategoryState = UserTestState.CurrentlySolving;
                    }

                }
            }
            return currentUserCategories;


        }
        public void SaveTest(UserTestsDTO test)
        {
            var testModel = mapper.MapTo<UserTests>(test);
            testModel.CreatedOn = dateTime.GetDateTimeNow();
            userTests.Add(testModel);
            saver.SaveChanges();
        }
        
        public SolveTestDTO GetCorrectSolveTest(string userId, string category)
        {
            if (this.UserStartedTest(userId, category))
            {
                var testId = this.GetTestIdFromUserIdAndCategory(userId, category);
                var test = testService.GetTestById(testId);
                var solveTestDto = mapper.MapTo<SolveTestDTO>(test);
                //shuffling start here /need to set the correct sequance of question ids in the answer views
                var collOfQuestionIds = new List<int>();
                foreach (var q in solveTestDto.Questions)
                {
                    collOfQuestionIds.Add(q.Id);
                }
                solveTestDto.CorrectOrderOfQuestionId = collOfQuestionIds;
                solveTestDto.Questions = shuffler.Shuffle(test.Questions.ToList());
                //
                var testAllowedTime = double.Parse(test.TimeInMinutes.ToString());
                var startedTime = this.StartedTestCreationTime(userId, category);
                if (this.TestIsSubmitted(userId, category))
                {
                    throw new CategoryDoneException();
                    //RedirectToAction("CategoryDone", "Solve")
                }
                var endTime = startedTime.Value.AddMinutes(testAllowedTime);
                var reaminingTime = Math.Round((endTime - dateTime.GetDateTimeNow()).TotalSeconds);
                solveTestDto.Category = category;
                solveTestDto.RemainingTime = int.Parse(reaminingTime.ToString());
                if (reaminingTime < 0)
                {
                    throw new TimeUpNeverSubmittedException();
                    //RedirectToAction("TimeUpNotSubmitted", "Solve")
                }
                solveTestDto.StorageOfAnswers = new List<string>();
                for (int i = 0; i < solveTestDto.Questions.Count; i++)
                {
                    solveTestDto.StorageOfAnswers.Add("No Answer");
                }
                return solveTestDto;
            }
            else
            {
                var categoryId = categoriesService.GetIdByCategoryName(category);
                var randomTest = testService.GetRandomTestFromCategory(categoryId);
                var solveTestDto = mapper.MapTo<SolveTestDTO>(randomTest);
                //shuffling start here /need to set the correct sequance of question ids in the answer views
                var collOfQuestionIds = new List<int>();
                foreach (var q in solveTestDto.Questions)
                {
                    collOfQuestionIds.Add(q.Id);
                }
                solveTestDto.CorrectOrderOfQuestionId = collOfQuestionIds;
                solveTestDto.Questions = shuffler.Shuffle(randomTest.Questions.ToList());
                // add the test in base on creation
                var saveThisTestCreation = new UserTestsDTO
                {
                    UserId = userId,
                    Category = category,
                    TestId = randomTest.Id
                };
                this.SaveTest(saveThisTestCreation);
                //added the up
                solveTestDto.StorageOfAnswers = new List<string>();
                for (int i = 0; i < solveTestDto.Questions.Count; i++)
                {
                    solveTestDto.StorageOfAnswers.Add("No Answer");
                }
                solveTestDto.Category = category;
                solveTestDto.CreatedOn = dateTime.GetDateTimeNow();
                solveTestDto.RemainingTime =
                Convert.ToInt32(((dateTime.GetDateTimeNow().AddMinutes(randomTest.TimeInMinutes) - dateTime.GetDateTimeNow()).TotalSeconds));
                return solveTestDto;
            }
        }
        public void ValidateAndAdd(SolveTestDTO solveTestDto, string userId)
        {
            var startedTime = this.StartedTestCreationTime(userId, solveTestDto.Category);
            var countDown = testService.GetTestCountDownByTestId(solveTestDto.Id);
            //giving the system 1 minute buffer for the test to submit later than the predicted time
            var executionTimeSeconds = (startedTime.Value.AddMinutes(countDown) - dateTime.GetDateTimeNow()).TotalSeconds;
            var timeRemain = (executionTimeSeconds + 60);
            var executionTimeMinutes = executionTimeSeconds / 60;
            if (timeRemain < 0)
            {
                throw new SubmittingLateException();
                //RedirectToAction("SubmittingLate", "Solve")
            }
            var completedTest = mapper.MapTo<UserTestsDTO>(solveTestDto);
            completedTest.TestId = completedTest.Id;
            completedTest.UserId = userId;
            completedTest.ExecutionTime = countDown - Math.Round(executionTimeMinutes, 2);
            this.Publish(completedTest);
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
        public UserTestsDTO GetUserTest(string email, int testId)
        {
            var userTest = this.userTests.All.Where(ut => ut.User.Email == email && ut.TestId == testId).
                                              Include(ut => ut.Test).
                                              ThenInclude(t =>t.Questions).
                                              ThenInclude(q => q.Answers).
                                              First();
            var userTestDto =  mapper.MapTo<UserTestsDTO>(userTest);
            return userTestDto;

        }
        public void RecalculateAllTestsScore()
        {
            var testsToRecalc = this.userTests.All.Where(x => x.Submitted);
            foreach (var item in testsToRecalc)
            {
                var itemDto = this.mapper.MapTo<UserTestsDTO>(item);
                decimal newScore = this.testService.GetResult(itemDto);
                item.Score = newScore;
            }
            saver.SaveChanges();
        }
        public void RecalculateTestScoreByTestId(int id)
        {
            var testsToRecalc = this.userTests.All.Where(t => t.TestId == id && t.Submitted);
            foreach (var item in testsToRecalc)
            {
                var itemDto = this.mapper.MapTo<UserTestsDTO>(item);
                decimal newScore = this.testService.GetResult(itemDto);
                item.Score = newScore;
            }
            saver.SaveChanges();
        }
    }
}

