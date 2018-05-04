using ITest.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Services.Data.Contracts
{
  public  interface IQuestionService
    {
        void Create(TestDTO dto);

    }
}
