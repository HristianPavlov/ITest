using ITest.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Services.Data
{
    public interface ITestRandomService
    {
        TestDTO GetRandomTestFromCategory(int categoryID);
        TestDTO GetTestById(int id);
        int GetTestIdFromUserIdAndCategory(string userId, string category);
        decimal GetResult(UserTestsDTO test);
        void Publish(UserTestsDTO test);
        void SaveTest(UserTestsDTO test);
        bool UserStartedTest(string userId, string category);
        DateTime? StartedTestCreationTime(string userId, string category);
    }
}
