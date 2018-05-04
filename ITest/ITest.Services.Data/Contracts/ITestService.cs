using ITest.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Services.Data
{
    public interface ITestService
    {
        TestDTO GetRandomTestFromCategory(Guid categoryID);
        TestDTO GetTestById(Guid id);
        TestDTO GetTestByName(string name);
        TestEditDTO GetTestByNameEditDTO(string name);
        int GetTestCountDownByTestId(Guid id);
        IEnumerable<TestDTO> GetAllTests();
        IEnumerable<TestEditDTO> GetAllTestsWithOutStuffInIttEditDTO();
    }
}
