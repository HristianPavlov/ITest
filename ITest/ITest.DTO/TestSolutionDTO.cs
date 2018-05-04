using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ITest.DTO
{
    public class TestSolutionDTO
    {
        public TestDTO Test { get; set; }
        //Key is question ID and Value is the answerId for that question
        public Dictionary<Guid, Guid> QuestionAnswers { get; set; }
    }
}
