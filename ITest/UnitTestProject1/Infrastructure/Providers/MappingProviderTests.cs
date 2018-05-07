using AutoMapper;
using ITest.Data.Models;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessTests.Infrastructure.Providers
{
    [TestClass]
    public class MappingProviderTests
    {
        [TestMethod]
        public void ProviderMapTo_CallsMapperMap()
        {
            //arrange
            var someItemToMap = "Hello";
            var mockMapper = new Mock<IMapper>();
            var fakeMappingProvide = new MappingProvider(mockMapper.Object);

            //act
            fakeMappingProvide.MapTo<int>(someItemToMap);

            //assert
            mockMapper.Verify(x => x.Map<int>(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ProviderProjectTo_CallsMapperMapCorrectTimes()
        {
            //arrange
            var someFakeCollection = new List<Category>
            {
                new Category(),
                new Category(),
                new Category(),
                new Category(),
                new Category()
            }.AsQueryable();

            var mockMapper = new Mock<IMapper>();
            var fakeMappingProvider = new MappingProvider(mockMapper.Object);

            //act
            fakeMappingProvider.ProjectTo<CategoryDTO>(someFakeCollection);
            //assert
            mockMapper.Verify(x => x.Map<CategoryDTO>(It.IsAny<Category>()), Times.Exactly(5));
        }

    }
}
