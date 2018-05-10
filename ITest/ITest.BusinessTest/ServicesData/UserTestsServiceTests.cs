using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using ITest.Services.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessTests.ServicesData
{
    [TestClass]
    public class UserTestsServiceTests
    {
        Mock<IMappingProvider> mapperMock;
        Mock<IDateTimeProvider> dateTimeProviderMock;
        Mock<ITestService> testServiceMock;
        Mock<ICategoriesService> categoriesServiceMock;
        Mock<IUserTestAnswersService> utaServiceMock;
        Mock<IRepository<UserTests>> repoUserTestsMock;
        Mock<IGenericShuffler> shufflerMock;
        Mock<ISaver> saverMock;

        Mock<IRandomProvider> randomMock;
        UserTestsService fakeUserTestsService;

        Guid correctGuid;
        Guid wrongGuid;
        string correctGuidAsString;
        string wrongGuidAsString;


        [TestInitialize]
        public void TestInitialize()
        {
            mapperMock = new Mock<IMappingProvider>();
            dateTimeProviderMock = new Mock<IDateTimeProvider>();
            testServiceMock = new Mock<ITestService>();
            categoriesServiceMock = new Mock<ICategoriesService>();
            utaServiceMock = new Mock<IUserTestAnswersService>();
            repoUserTestsMock = new Mock<IRepository<UserTests>>();
            shufflerMock = new Mock<IGenericShuffler>();
            saverMock = new Mock<ISaver>();

            correctGuid = Guid.Parse("495f7e92-d4ae-4299-9340-b300d4242f57");
            wrongGuid = Guid.Parse("2b932e9d-95e3-48db-8fbd-dee921dc6795");
            correctGuidAsString = "495f7e92-d4ae-4299-9340-b300d4242f57";
            wrongGuidAsString = "2b932e9d-95e3-48db-8fbd-dee921dc6795";

            fakeUserTestsService = new UserTestsService
                (mapperMock.Object,
                dateTimeProviderMock.Object,
                testServiceMock.Object,
                categoriesServiceMock.Object,
                utaServiceMock.Object,
                repoUserTestsMock.Object,
                shufflerMock.Object,
                saverMock.Object);
        }

        [TestMethod]
        public void SaveTest_ThrowWhen_UserTestDtoNull()
        {
            //arrange & act & asser
            Assert.ThrowsException<ArgumentNullException>(() => fakeUserTestsService.SaveTest(null));
        }
        [TestMethod]
        public void SaveTest_Calls_MapperMap()
        {
            //arrange
            var someUserTestDto = new UserTestsDTO();
            var fakeUserTest = new UserTests();
            mapperMock.Setup(x => x.MapTo<UserTests>(It.IsAny<UserTestsDTO>())).Returns(fakeUserTest);
            // act
            fakeUserTestsService.SaveTest(someUserTestDto);
            // assert
            mapperMock.Verify(x => x.MapTo<UserTests>(It.IsAny<UserTestsDTO>()), Times.Once);
        }
        [TestMethod]
        public void SaveTest_Calls_GetDateTimeNow()
        {
            //arrange
            var someUserTestDto = new UserTestsDTO();
            var fakeUserTest = new UserTests();
            mapperMock.Setup(x => x.MapTo<UserTests>(It.IsAny<UserTestsDTO>())).Returns(fakeUserTest);
            // act
            fakeUserTestsService.SaveTest(someUserTestDto);
            // assert
            dateTimeProviderMock.Verify(x => x.GetDateTimeNow(), Times.Once);
        }
        [TestMethod]
        public void SaveTest_Calls_RepoAdd()
        {
            //arrange
            var someUserTestDto = new UserTestsDTO();
            var fakeUserTest = new UserTests();
            mapperMock.Setup(x => x.MapTo<UserTests>(It.IsAny<UserTestsDTO>())).Returns(fakeUserTest);
            // act
            fakeUserTestsService.SaveTest(someUserTestDto);
            // assert
            repoUserTestsMock.Verify(x => x.Add(It.IsAny<UserTests>()), Times.Once);
        }
        [TestMethod]
        public void SaveTest_Calls_SaverSaveChanges()
        {
            //arrange
            var someUserTestDto = new UserTestsDTO();
            var fakeUserTest = new UserTests();
            mapperMock.Setup(x => x.MapTo<UserTests>(It.IsAny<UserTestsDTO>())).Returns(fakeUserTest);
            // act
            fakeUserTestsService.SaveTest(someUserTestDto);
            // assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once);
        }
        //VALIDATE AND ADD TESTS HERE
        [TestMethod]
        public void Publish_ThrowWhen_UserTestDtoNull()
        {
            //arrange & act & asser
            Assert.ThrowsException<ArgumentNullException>(() => fakeUserTestsService.Publish(null));
        }

        [TestMethod]
        public void Publish_Calls_MapperMap()
        {
            //arrange
            var categoryName = "Petko";
            var someUserTestDto = new UserTestsDTO
            {
                Category = categoryName,
                UserId = correctGuidAsString
            };
            var fakeUserTest = new UserTests();
            mapperMock.Setup(x => x.MapTo<UserTests>(It.IsAny<UserTestsDTO>())).Returns(fakeUserTest);
            var fakeUserTests = new List<UserTests>
            {
                new UserTests
                {
                    UserId = correctGuidAsString,
                    Id = wrongGuid,
                    Test = new Test
                    {
                        Category = new Category
                        {
                            Name=categoryName
                        }
                    }

                }
            }.AsQueryable();
            repoUserTestsMock.Setup(x => x.All).Returns(fakeUserTests);
            // act
            fakeUserTestsService.Publish(someUserTestDto);
            // assert
            mapperMock.Verify(x => x.MapTo<UserTests>(It.IsAny<UserTestsDTO>()), Times.Once);
        }
        [TestMethod]
        public void Publish_Calls_UtaServiceSaveChanges()
        {
            //arrange
            var categoryName = "Petko";
            var someUserTestDto = new UserTestsDTO
            {
                Category = categoryName,
                UserId = correctGuidAsString
            };
            var fakeUserTest = new UserTests();
            mapperMock.Setup(x => x.MapTo<UserTests>(It.IsAny<UserTestsDTO>())).Returns(fakeUserTest);
            var fakeUserTests = new List<UserTests>
            {
                new UserTests
                {
                    UserId = correctGuidAsString,
                    Id = wrongGuid,
                    Test = new Test
                    {
                        Category = new Category
                        {
                            Name=categoryName
                        }
                    }

                }
            }.AsQueryable();
            repoUserTestsMock.Setup(x => x.All).Returns(fakeUserTests);
            // act
            fakeUserTestsService.Publish(someUserTestDto);
            // assert
            utaServiceMock.Verify(x => x.SaveQuestionAnswers(It.IsAny<UserTestsDTO>()), Times.Once);
        }
        [TestMethod]
        public void GetUserTestId_ThrowWhen_UserIdNull()
        {
            //arrange
            var categoryName = "Petko";
            var someUserTestDto = new UserTestsDTO
            {
                Category = categoryName,
            };
            var fakeUserTest = new UserTests();
            mapperMock.Setup(x => x.MapTo<UserTests>(It.IsAny<UserTestsDTO>())).Returns(fakeUserTest);
            // act & assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeUserTestsService.Publish(someUserTestDto));
        }
        [TestMethod]
        public void GetUserTestId_ThrowWhen_CategoryNameIsNull()
        {
            //arrange
            var someUserTestDto = new UserTestsDTO
            {
                UserId = correctGuidAsString
            };
            var fakeUserTest = new UserTests();
            mapperMock.Setup(x => x.MapTo<UserTests>(It.IsAny<UserTestsDTO>())).Returns(fakeUserTest);
            // act & assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeUserTestsService.Publish(someUserTestDto));
        }

        //[TestMethod]
        //public void Publish_Calls_UtaServiceSaveChanges()
        //{
        //    //arrange
        //    var categoryName = "Petko";
        //    var someUserTestDto = new UserTestsDTO
        //    {
        //        Category = categoryName,
        //        UserId = correctGuidAsString
        //    };
        //    var fakeUserTest = new UserTests();
        //    mapperMock.Setup(x => x.MapTo<UserTests>(It.IsAny<UserTestsDTO>())).Returns(fakeUserTest);
        //    var fakeUserTests = new List<UserTests>
        //    {
        //        new UserTests
        //        {
        //            UserId = correctGuidAsString,
        //            Id = wrongGuid,
        //            Test = new Test
        //            {
        //                Category = new Category
        //                {
        //                    Name=categoryName
        //                }
        //            }

        //        }
        //    }.AsQueryable();
        //    repoUserTestsMock.Setup(x => x.All).Returns(fakeUserTests);
        //    // act
        //    fakeUserTestsService.Publish(someUserTestDto);
        //    // assert
        //    utaServiceMock.Verify(x => x.SaveQuestionAnswers(It.IsAny<UserTestsDTO>()), Times.Once);
        //}
    }
}
