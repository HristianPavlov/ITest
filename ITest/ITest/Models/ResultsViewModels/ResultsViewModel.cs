using ITest.Data.Models;
using ITest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.ResultsViewModels
{
    public class ResultsViewModel
    {
        public string UserEmail { get; set; }

        public TestDTO Test { get; set; }

        public decimal Score { get; set; }

        public string Category { get; set; }

        public double ExecutionTime { get; set; }
    }
}
