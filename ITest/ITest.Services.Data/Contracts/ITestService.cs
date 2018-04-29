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
        int GetTestCountDownByTestId(int id);
        decimal GetResult(UserTestsDTO test);
    }
}
