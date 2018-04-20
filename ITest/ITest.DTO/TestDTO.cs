using ITest.Data.Models.Enums;
using System.Collections.Generic;

namespace ITest.DTO
{
    public class TestDTO
    {
        public string AuthorId { get; set; }

        public UserDTO Author { get; set; }

        public TestStatus Status { get; set; }

        public ICollection<QuestionDTO> Questions { get; set; }

        public ICollection<UserTestsDTO> Users { get; set; }

        public int CategoryId { get; set; }

        public CategoryDTO Category { get; set; }
    }
}