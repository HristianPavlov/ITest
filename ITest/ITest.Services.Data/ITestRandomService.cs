using ITest.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Services.Data
{
    public interface ITestRandomService
    {
        IEnumerable<UserTestsDTO> GetAllUserTests();
        bool TestIsSubmitted(string userId, string category);
        TestDTO GetRandomTestFromCategory(int categoryID);
        TestDTO GetTestById(int id);
        int GetTestIdFromUserIdAndCategory(string userId, string category);
        int GetTestCountDownByTestId(int id);
        decimal GetResult(UserTestsDTO test);
        void Publish(UserTestsDTO test);
        void SaveTest(UserTestsDTO test);
        bool UserStartedTest(string userId, string category);
        DateTime? StartedTestCreationTime(string userId, string category);
    }
}
