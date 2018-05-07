using AutoMapper;
using AutoMapper.QueryableExtensions;
using ITest.Data.Models;
using ITest.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ITest.Infrastructure.Providers
{
    public class MappingProvider : IMappingProvider
    {
        private IMapper mapper;

        public MappingProvider(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public TDestination MapTo<TDestination>(object source)
        {
            return this.mapper.Map<TDestination>(source);
        }
        //public IEnumerable<TDestination> ProjectTo<TDestination>(IQueryable<object> source)
        //{
        //    var toReturn = source.ProjectTo<TDestination>();
        //    return toReturn;
        //}
        public IEnumerable<TDestination> ProjectTo<TDestination>(IQueryable<object> source)
        {
            var collToReturn = new Collection<TDestination>();
            foreach (var item in source)
            {
                collToReturn.Add(this.MapTo<TDestination>(item));
            }
            return collToReturn;
        }

        public IEnumerable<TDestination> EnumProjectTo<TDestination>(IEnumerable<object> source)
        {
            return source.AsQueryable().ProjectTo<TDestination>();
        }
        public IQueryable<TDestination> QueryableProjectTo<TDestination>(IQueryable<object> source)
        {
            return source.ProjectTo<TDestination>();
        }
        public IEnumerable<TDestination> EnumerableProjectTo<TSource, TDestination>(IEnumerable<TSource> source)
        {
            // AsQuryable cast to avoid query materialization errors
            return source.AsQueryable().ProjectTo<TDestination>();
        }

        public TDestination MapTo<TSource, TDestination>(TSource source, TDestination destination)
        {
            return this.mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
