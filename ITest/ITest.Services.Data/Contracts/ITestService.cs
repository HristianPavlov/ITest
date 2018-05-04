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
        int GetTestCountDownByTestId(Guid id);
    }
}
