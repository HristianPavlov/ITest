using ITest.Data.Models;
using ITest.DTO;

namespace ITest.Services.Data
{
    public interface ICreateTestService
    {
        void Create(TestDTO dto);
         void Update(TestEditDTO dto);
        void PublishedUpdate(TestEditDTO dto);
        void DeleteTest(string name);
         void DeleteQuestion(Question q);
        void Disable(string name);
        void Publish(string name);
    }
}