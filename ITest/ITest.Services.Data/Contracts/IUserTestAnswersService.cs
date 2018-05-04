
using ITest.DTO;
using System;

namespace ITest.Services.Data
{
    public interface IUserTestAnswersService
    {
        void SaveQuestionAnswers(UserTestsDTO test);
        decimal GetResult(string userId, string category);
        void RecalculateAllTakenTestsWithId(Guid testId);
    }
}