
using ITest.DTO;

namespace ITest.Services.Data
{
    public interface IUserTestAnswersService
    {
        void SaveQuestionAnswers(UserTestsDTO test);
        decimal GetResult(string userId, string category);
    }
}