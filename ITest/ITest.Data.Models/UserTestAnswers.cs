using ITest.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Data.Models
{
    public class UserTestAnswers : DataModel
    {
        public Guid UserTestId { get; set; }
        public UserTests UserTest { get; set; }

        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
