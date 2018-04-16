using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Data.Models.Abstracts
{
    public interface IAuditable
    {
         DateTime? CreatedOn { get; set; }

         DateTime? ModifiedOn { get; set; }
    }
}
