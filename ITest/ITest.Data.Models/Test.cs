using ITest.Data.Models.Abstracts;
using ITest.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ITest.Data.Models
{
    public class Test : DataModel
    {
      

        public string Name { get; set; }

        public string AuthorId { get; set; }
        public User Author { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public TestStatus Status { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<UserTests> Users { get; set; }


        public int TimeInMinutes { get; set; }
    }
}
