using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Data.Models.Abstracts
{
    public interface IDeletable
    {
         bool IsDeleted { get; set; }

         DateTime? DeletedOn { get; set; }

    }
}
