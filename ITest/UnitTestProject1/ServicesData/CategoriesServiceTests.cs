using ITest.Data.Models;
using ITest.Data.Models.Enums;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.DTO.Enums;
using ITest.Infrastructure.Providers;
using ITest.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BusinessTests.ServicesData
{
    [TestClass]
    public class CategoriesServiceTests
    {
        Mock<IMappingProvider> mapperMock;
        Mock<IRepository<Category>> repoCategoriesMock;
        Mock<ISaver> saverMock;
        CategoriesService fakeCategoriesService;
        Guid someGuid;

        [TestInitialize]
        public void TestInitialize()
        {
            mapperMock = new Mock<IMappingProvider>();
            repoCategoriesMock = new Mock<IRepository<Category>>();
            saverMock = new Mock<ISaver>();
            someGuid = Guid.Parse("495f7e92-d4ae-4299-9340-b300d4242f57");
            fakeCategoriesService = new CategoriesService(mapperMock.Object, repoCategoriesMock.Object, saverMock.Object);
        }

        [TestMethod]
        public void Add_ShouldThrow_WhenPassedDtoNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => fakeCategoriesService.Add(null));
        }

        [TestMethod]
        public void Add_Should_CallMapper()
        {
            //arrange
            var fakeDto = new CategoryDTO();

            //act
            fakeCategoriesService.Add(fakeDto);

            //assert
            mapperMock.Verify(x => x.MapTo<Category>(It.IsAny<CategoryDTO>()), Times.Once);
        }

        [TestMethod]
        public void Add_Should_CallRepoAdd()
        {
            //arrange
            var fakeDto = new CategoryDTO();

            //act
            fakeCategoriesService.Add(fakeDto);

            //assert
            repoCategoriesMock.Verify(x => x.Add(It.IsAny<Category>()), Times.Once);
        }

        [TestMethod]
        public void Add_Should_CallSaverSave()
        {
            //arrange
            var fakeDto = new CategoryDTO();

            //act
            fakeCategoriesService.Add(fakeDto);

            //assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void GetIdByName_ShouldThrow_WhenCategoryNameEmpty()
        {
            //arrange && act && assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeCategoriesService.GetIdByCategoryName(null));

        }

        [TestMethod]
        public void GetIdByName_Should_CallRepoAll()
        {
            //arrange
            var someCategory = "Hello";
            var all = new List<Category>()
            {
                new Category { Name=someCategory}
            };
            repoCategoriesMock.Setup(x => x.All).Returns(all.AsQueryable());
            //act
            fakeCategoriesService.GetIdByCategoryName(someCategory);

            //assert
            repoCategoriesMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetIdByName_Should_ReturnsCorrectId()
        {
            //arrange
            var someCategory = "Hello";
            var expectedId = new Guid();
            var all = new List<Category>()
            {
                new Category
                {
                    Name =someCategory,
                    Id= expectedId
                }
            };
            repoCategoriesMock.Setup(x => x.All).Returns(all.AsQueryable());
            //act
            var foundId = fakeCategoriesService.GetIdByCategoryName(someCategory);

            //assert
            Assert.AreEqual(expectedId, foundId);
        }

        [TestMethod]
        public void GetAllCategories_ShouldCall_RepoAll()
        {
            //arrange && act
            fakeCategoriesService.GetAllCategories();

            //assert
            repoCategoriesMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetAllCategories_ShouldCall_MapperProjectTo()
        {
            //arrange 
            var all = new List<Category>().AsQueryable();
            repoCategoriesMock.Setup(x => x.All).Returns(all.AsQueryable());

            //act
            fakeCategoriesService.GetAllCategories();
            //assert
            mapperMock.Verify(x => x.ProjectTo<CategoryDTO>(all), Times.Once);
        }

        [TestMethod]
        public void GetAllCategories_ShouldSetCorrectCategoryStatus()
        {
            //arrange 
            var all = new List<Category>
            {
                //this category will have a legit test
                //new Category
                //{
                //    Tests = new Collection<Test>
                //    {
                //        new Test
                //        {
                //            //
                //             Status = TestStatus.Published,
                //             IsDeleted = false
                //        }
                //    }
                //},

                ////this category will have unpublished test
                //new Category
                //{
                //    Tests = new Collection<Test>
                //    {
                //        new Test
                //        {
                //            //
                //             Status = TestStatus.Draft,
                //             IsDeleted = false
                //        }
                //    }
                //},

                ////this category will have disabled test
                //new Category
                //{
                //    Tests = new Collection<Test>
                //    {
                //        new Test
                //        {
                //            //
                //             Status = TestStatus.Disabled,
                //             IsDeleted = false
                //        }
                //    }
                //},

                ////this category will have deleted/published
                //new Category
                //{
                //    Tests = new Collection<Test>
                //    {
                //        new Test
                //        {
                //            //
                //             Status = TestStatus.Published,
                //             IsDeleted = true
                //        }
                //    }
                //}

            }.AsQueryable();
            var allMapped = new List<CategoryDTO>
            {
                //this category will have a legit test
                new CategoryDTO
                {
                    Tests = new Collection<TestDTO>
                    {
                        new TestDTO
                        {
                            //
                             Status = TestStatus.Published,
                             IsDeleted = false
                        }
                    }
                },

                //this category will have unpublished test
                new CategoryDTO
                {
                    Tests = new Collection<TestDTO>
                    {
                        new TestDTO
                        {
                            //
                             Status = TestStatus.Draft,
                             IsDeleted = false
                        }
                    }
                },

                //this category will have disabled test
                new CategoryDTO
                {
                    Tests = new Collection<TestDTO>
                    {
                        new TestDTO
                        {
                            //
                             Status = TestStatus.Disabled,
                             IsDeleted = false
                        }
                    }
                },

                //this category will have deleted/published
                new CategoryDTO
                {
                    Tests = new Collection<TestDTO>
                    {
                        new TestDTO
                        {
                            //
                             Status = TestStatus.Published,
                             IsDeleted = true
                        }
                    }
                }
            };

            var readyCategoriesExpected = 1;
            var emptyCategoriesExpected = 3;
            repoCategoriesMock.Setup(x => x.All).Returns(all);
            mapperMock.Setup(x => x.ProjectTo<CategoryDTO>( It.IsAny<IQueryable<Category>>()) ).Returns(allMapped);

            //act
            var foundCategories = fakeCategoriesService.GetAllCategories();

            //assert
            //should give me 3 empty categories and 1 ready to be started
            Assert.AreEqual(readyCategoriesExpected, foundCategories.Where(x => x.CategoryState == UserTestState.Start).Count());
            Assert.AreEqual(emptyCategoriesExpected, foundCategories.Where(x => x.CategoryState == UserTestState.CategoryEmpty).Count());

        }
    }
}
