using ITest.DTO;
using System;
using System.Collections.Generic;

namespace ITest.Services.Data
{
    public interface IUserTestsService
    {
        bool UserStartedTest(string userId, string category);
        bool TestIsSubmitted(string userId, string category);
        TestDTO GetTestFromUserIdAndCategory(string userId, string category);
        Guid GetUserTestId(string userId, string category);
        IEnumerable<UserTestsDTO> GetAllUserTests();
        IEnumerable<UserTestsDTO> GetCurrentUserTests(string userId);
        void SaveTest(UserTestsDTO test);
        void Publish(UserTestsDTO test);
        SolveTestDTO GetCorrectSolveTest(string userId, string category);
        void ValidateAndAdd(SolveTestDTO solveTestDto, string userId);
        UserTestsDTO GetUserTest(string email, Guid testId);
        DateTime? StartedTestCreationTime(string userId, string category);
        TestSolutionDTO GetDetailedSolution(string userEmail, Guid testId);
        //should make it work
        //void RecalculateAllTestsScore();
        //void RecalculateTestScoreByTestId(Guid id);
    }
}