using ITest.Data.Models;
using ITest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITest.Infrastructure.Providers
{
   public interface IMappingProvider
    {
        TDestination MapTo<TDestination>(object source);

        IEnumerable<TDestination> ProjectTo<TDestination>(IQueryable<object> source);

        IEnumerable<TDestination> EnumProjectTo<TDestination>(IEnumerable<object> source);
    }
}
