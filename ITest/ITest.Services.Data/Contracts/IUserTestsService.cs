using ITest.DTO;
using System;
using System.Collections.Generic;

namespace ITest.Services.Data
{
    public interface IUserTestsService
    {
        IEnumerable<UserTestsDTO> GetAllUserTests();
        void SaveTest(UserTestsDTO test);
        void Publish(UserTestsDTO test);
        void ValidateAndAdd(SolveTestDTO solveTestDto, string userId);
        TestSolutionDTO GetDetailedSolution(string userEmail, Guid testId);
        IEnumerable<UserTestsDTO> GetCurrentUserTests(string userId);
        SolveTestDTO GetCorrectSolveTest(string userId, string category);
    }
}