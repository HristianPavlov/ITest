using ITest.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Services.Data
{
    public interface ITestRandomService
    {
        TestDTO GetRandomTestFromCategory(int categoryID);
        
    }
}
