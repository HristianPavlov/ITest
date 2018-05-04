using ITest.Models.TestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.ResultsViewModels
{
    public class DetailedTestViewModel
    {
        public ShowTestViewModel Test { get; set; }
        public Dictionary<Guid, Guid> QuestionAnswers { get; set; }
    }
}
