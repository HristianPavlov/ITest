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

        public Guid UserTestId { get; set; }

        public string UserEmail { get; set; }

        public Guid TestId { get; set; }

        public TestDTO Test { get; set; }

        public decimal Score { get; set; }

        public int NumberOfQuestions { get; set; }

        public string Category { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool Submitted { get; set; }

        public double ExecutionTime { get; set; }

        public ICollection<QuestionDTO> Questions { get; set; }

        //Think about this id
        public Guid Id { get; set; }
       
        public UserTestState CategoryState { get; set; }
    }
}
