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
        void SaveTest(UserTestsDTO test);
        void Publish(UserTestsDTO test);
    }
}