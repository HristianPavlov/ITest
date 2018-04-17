using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Data.UnitOfWork
{
    public interface ISaver
    {
        void SaveChanges();

        void SaveChangesAsync();
    }
}
