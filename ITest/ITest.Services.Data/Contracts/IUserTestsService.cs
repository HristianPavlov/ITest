using ITest.DTO;
using System;
using System.Collections.Generic;

namespace ITest.Services.Data
{
    public interface IUserTestsService
    {
        bool UserStartedTest(string userId, string category);
        DateTime? StartedTestCreationTime(string userId, string category);
        bool TestIsSubmitted(string userId, string category);
        int GetTestIdFromUserIdAndCategory(string userId, string category);
        IEnumerable<UserTestsDTO> GetAllUserTests();
        IEnumerable<UserTestsDTO> GetCurrentUserTests(string userId);
        void SaveTest(UserTestsDTO test);
        void Publish(UserTestsDTO test);
        SolveTestDTO GetCorrectSolveTest(string userId, string category);
        void ValidateAndAdd(SolveTestDTO solveTestDto, string userId);
        void RecalculateAllTestsScore();
        void RecalculateTestScoreByTestId(int id);
        UserTestsDTO GetUserTest(string email, int testId);
    }
}