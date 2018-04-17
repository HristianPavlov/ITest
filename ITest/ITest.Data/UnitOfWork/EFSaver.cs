using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Data.UnitOfWork
{

    //async class and  method to let the system know what is comming???
  public  class EFSaver : ISaver
    {
        private readonly ITestDbContext context;

        public EFSaver(ITestDbContext context)
        {
            this.context = context;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public async void SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
