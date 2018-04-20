using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ITest.Data.Models.Abstracts
{
    public abstract class DataModel : IAuditable, IDeletable
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
