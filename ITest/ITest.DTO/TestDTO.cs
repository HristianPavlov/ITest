using ITest.Data.Models;
using System.Collections.Generic;

namespace ITest.DTO
{
    public class TestDTO
    {

        public int Id { get; set; }

     

        public string AuthorId { get; set; }

        public AuthorDTO Author { get; set; }

        public TestStatusDTO Status { get; set; }

        public ICollection<QuestionDTO> Questions { get; set; }

        public ICollection<UserTestsDTO> Users { get; set; }
    }
}