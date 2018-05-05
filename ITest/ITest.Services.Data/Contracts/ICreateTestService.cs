using ITest.DTO;

namespace ITest.Services.Data
{
    public interface ICreateTestService
    {
        void Create(TestDTO dto);
         void Update(TestEditDTO dto);
        void PublishedUpdate(TestEditDTO dto);
    }
}