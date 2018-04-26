using ITest.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ITest.Data.Models
{
   public class UserTests:IAuditable,IDeletable

    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int TestId { get; set; }

        public Test Test { get; set; }

        public decimal Score { get; set; }

        public string SerializedAnswers { get; set; }

        public bool IsDeleted { get; set; }

        public string Category { get; set; }

        public bool Submitted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
