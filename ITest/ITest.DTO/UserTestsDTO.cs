using ITest.Data.Models;
using ITest.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.DTO
{
    public class UserTestsDTO
    {
        public string UserId { get; set; }

        public int TestId { get; set; }

        public TestDTO Test { get; set; }

        public decimal Score { get; set; }

        public List<string> StorageOfAnswers { get; set; }

        public string Category { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool Submitted { get; set; }

        public double ExecutionTime { get; set; }

        //Think about this id
        public int Id { get; set; }

        public UserTestState CategoryState { get; set; }

    }
}
