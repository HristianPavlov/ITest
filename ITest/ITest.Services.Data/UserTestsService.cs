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
        private readonly IUserTestAnswersService utaService;
        private readonly IRepository<UserTests> userTests;
        private readonly IGenericShuffler shuffler;
        private readonly ISaver saver;

        public UserTestsService(IMappingProvider mapper,
                                IDateTimeProvider dateTime,
                                ITestService testService,
                                ICategoriesService categoriesService,
                                IUserTestAnswersService utaService,
                                IRepository<UserTests> userTests,
                                IGenericShuffler shuffler,
                                ISaver saver)
        {
            this.mapper = mapper;
            this.dateTime = dateTime;
            this.testService = testService;
            this.categoriesService = categoriesService;
            this.utaService = utaService;
            this.userTests = userTests;
            this.shuffler = shuffler;
            this.saver = saver;
        }
        public TestSolutionDTO GetDetailedSolution(string userEmail, Guid testId)
        {
            var userTest = userTests.All.Where(ut => ut.TestId == testId && ut.User.Email == userEmail)
                .Include(ut => ut.Test)
                    .ThenInclude(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                .Include(ut => ut.Answers)
                    .ThenInclude(uta => uta.Answer)
                .First();
            var userTestAnswers = userTest.Answers;
            var testSolutionDto = mapper.MapTo<TestSolutionDTO>(userTest);
            testSolutionDto.QuestionAnswers = new Dictionary<Guid, Guid>();
            foreach (var uta in userTestAnswers)
            {
                testSolutionDto.QuestionAnswers.Add(uta.Answer.QuestionId, uta.Answer.Id);
            }
            return testSolutionDto;
        }
        private bool UserStartedTest(string userId, string category)
        {
            if (this.userTests.All.Any(x => x.UserId == userId && x.Test.Category.Name == category))
            {
                return true;
            }
            return false;
        }
        private DateTime? StartedTestCreationTime(string userId, string category)
        {
            return userTests.All.First(x => x.UserId == userId && x.Test.Category.Name == category).CreatedOn;
        }
        private bool TestIsSubmitted(string userId, string category)
        {
            return userTests.All.First(x => x.UserId == userId && x.Test.Category.Name == category).Submitted;
        }
        private TestDTO GetTestFromUserIdAndCategory(string userId, string category)
        {
            var testsFromThisCategory = userTests.All.Where(x => x.UserId == userId && x.Test.Category.Name == category)
                .Include(ut => ut.Test)
                    .ThenInclude(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                .First();
            var testDtoToReturn = mapper.MapTo<TestDTO>(testsFromThisCategory.Test);
            testDtoToReturn.CreatedOn = testsFromThisCategory.CreatedOn;
            return testDtoToReturn;
        }
        private Guid GetUserTestId(string userId, string category)
        {
            return userTests.All.First(x => x.UserId == userId && x.Test.Category.Name == category).Id;
        }
        public IEnumerable<UserTestsDTO> GetAllUserTests()
        {
            var allUserTests = userTests.All.
                                        Include(t => t.Test).
                                             ThenInclude(t => t.Category).
                                        Include(u => u.User);

            return mapper.ProjectTo<UserTestsDTO>(allUserTests);

        }
        public IEnumerable<UserTestsDTO> GetCurrentUserTests(string userId)
        {
            var currentUserTests = userTests.All.Where(u => u.UserId == userId).
                                        Include(t => t.Test).
                                            ThenInclude(x => x.Category).
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
                var test = this.GetTestFromUserIdAndCategory(userId, category);
                var solveTestDto = mapper.MapTo<SolveTestDTO>(test);
                solveTestDto.Questions = shuffler.Shuffle(test.Questions.ToList());
                var testAllowedTime = Convert.ToDouble(test.TimeInMinutes);
                var startedTime = test.CreatedOn;
                if (this.TestIsSubmitted(userId, category))
                {
                    throw new CategoryDoneException();
                    //RedirectToAction("CategoryDone", "Solve")
                }
                var endTime = startedTime.Value.AddMinutes(testAllowedTime);
                var remainingTime = Math.Round((endTime - dateTime.GetDateTimeNow()).TotalSeconds);
                solveTestDto.Category = category;
                solveTestDto.RemainingTime = int.Parse(remainingTime.ToString());
                if (remainingTime < 0)
                {
                    throw new TimeUpNeverSubmittedException();
                    //RedirectToAction("TimeUpNotSubmitted", "Solve")
                }
                return solveTestDto;
            }
            else
            {
                var categoryId = categoriesService.GetIdByCategoryName(category);
                var randomTest = testService.GetRandomTestFromCategory(categoryId);
                var solveTestDto = mapper.MapTo<SolveTestDTO>(randomTest);
                //shuffling start here 
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
                solveTestDto.Category = category;
                solveTestDto.CreatedOn = dateTime.GetDateTimeNow();
                Convert.ToInt32(((dateTime.GetDateTimeNow().AddMinutes(randomTest.TimeInMinutes) - dateTime.GetDateTimeNow()).TotalSeconds));
                solveTestDto.RemainingTime = randomTest.TimeInMinutes * 60;//in seconds
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
            completedTest.ExecutionTime = Math.Round(countDown - executionTimeMinutes, 3);
            this.Publish(completedTest);
        }
        public void Publish(UserTestsDTO test)
        {
            var testModel = mapper.MapTo<UserTests>(test);
            test.UserTestId = this.GetUserTestId(test.UserId, test.Category);
            utaService.SaveQuestionAnswers(test);
            var score = this.utaService.GetResult(test.UserId, test.Category);

            //testModel.Score = score;
            //userTests.Update(testModel);
            //saver.SaveChanges();
            //Update doesnt work "cannot track many instances of same type"

            var updatingtest = userTests.All.FirstOrDefault(x => x.TestId == test.TestId && x.UserId == test.UserId);
            updatingtest.ExecutionTime = testModel.ExecutionTime;
            updatingtest.Score = score;
            updatingtest.Submitted = true;
            saver.SaveChanges();
        }
        private UserTestsDTO GetUserTest(string email, Guid testId)
        {
            var userTest = this.userTests.All.Where(ut => ut.User.Email == email && ut.TestId == testId).
                                              Include(ut => ut.Test).
                                              ThenInclude(t => t.Questions).
                                              ThenInclude(q => q.Answers).
                                              First();
            var userTestDto = mapper.MapTo<UserTestsDTO>(userTest);
            return userTestDto;
        }
    }
}

