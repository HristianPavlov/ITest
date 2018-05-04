using ITest.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.DTO
{
   public class TestEditDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        
        public TestStatus Status { get; set; }

        public IList<QuestionEditDTO> Questions { get; set; }
      

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }


        public int TimeInMinutes { get; set; }
    }
}
