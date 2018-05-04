using ITest.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Services.Data
{
    public interface ITestService
    {
        TestDTO GetRandomTestFromCategory(int categoryID);
        TestDTO GetTestById(int id);
        TestDTO GetTestByName(string name);
        TestEditDTO GetTestByNameEditDTO(string name);

        int GetTestCountDownByTestId(int id);

        decimal GetResult(UserTestsDTO test);
        IEnumerable<TestDTO> GetAllTests();
        IEnumerable<TestEditDTO> GetAllTestsWithOutStuffInIttEditDTO();
    }
}
