using ITest.Data.Models.Enums;
using System;
using System.Collections.Generic;

namespace ITest.DTO
{
    public class TestDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string AuthorId { get; set; }

        public TestStatus Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public ICollection<QuestionDTO> Questions { get; set; }

        public ICollection<UserTestsDTO> Users { get; set; }

        public Guid CategoryId { get; set; }

        public CategoryDTO Category { get; set; }
    
        public int TimeInMinutes { get; set; }

        public bool IsDeleted { get; set; }
    }
}