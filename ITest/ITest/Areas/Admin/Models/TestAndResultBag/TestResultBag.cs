using ITest.Models.ResultsViewModels;
using ITest.Models.TestRealBagViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Areas.Admin.Models.TestAndResultBag
{
    public class TestResultBag
    {
        public TestRealBagViewModel Test { get; set; }
        public  ResultBagViewModel Results { get; set; }
    }
}
