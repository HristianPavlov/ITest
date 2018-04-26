using ITest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.ResultsViewModels
{
    public class ResultsViewModel
    {
        public User User { get; set; }

        public Test Test { get; set; }

        public decimal Score { get; set; }

        public string Category { get; set; }

        public double ExecutionTime { get; set; }
    }
}
