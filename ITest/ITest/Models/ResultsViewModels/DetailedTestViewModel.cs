using ITest.Models.TestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.ResultsViewModels
{
    public class DetailedTestViewModel
    {
        public TestViewModel Test { get; set; }
        public List<string> StorageOfAnswers { get; set; }
    }
}
