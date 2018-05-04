using ITest.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ITest.Data.Models
{
    public class UserTests : DataModel
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public Guid TestId { get; set; }
        public Test Test { get; set; }

        public ICollection<UserTestAnswers> Answers { get; set; }

        public decimal Score { get; set; }

        public double ExecutionTime { get; set; }

        public bool Submitted { get; set; }

    }
}
