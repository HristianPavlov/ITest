using ITest.Data.Models;
using ITest.Data.Models.Enums;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.Infrastructure.CustomExceptions;
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
    public class TestServiceTests
    {
        Mock<IMappingProvider> mapperMock;
        Mock<IRepository<Test>> repoTestMock;
        Mock<ISaver> saverMock;
        Mock<IRandomProvider> randomMock;
        TestService fakeTestService;
        Guid correctGuid;
        Guid wrongGuid;

        [TestInitialize]
        public void TestInitialize()
        {
            mapperMock = new Mock<IMappingProvider>();
            repoTestMock = new Mock<IRepository<Test>>();
            saverMock = new Mock<ISaver>();
            randomMock = new Mock<IRandomProvider>();
            correctGuid = Guid.Parse("495f7e92-d4ae-4299-9340-b300d4242f57");
            wrongGuid = Guid.Parse("2b932e9d-95e3-48db-8fbd-dee921dc6795");

            fakeTestService = new TestService(mapperMock.Object, repoTestMock.Object, randomMock.Object, saverMock.Object);
        }

        [TestMethod]
        public void GetTestCountDownId_Throw_WhenIdEmpty()
        {
            //arrange & act & assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeTestService.GetTestCountDownByTestId(Guid.Empty));
        }

        [TestMethod]
        public void GetTestCountDownById_Call_RepoAll()
        {
            //arrange & act 
            fakeTestService.GetAllTests();

            //assert
            repoTestMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetTestCountDownById_ReturnsCorrectValue()
        {
            //arrange
            var testId = correctGuid;
            var expectedCountDown = 20;

            var all = new List<Test>
            {
                new Test
                {
                    Id=testId,
                    TimeInMinutes=expectedCountDown
                }
            };
            repoTestMock.Setup(x => x.All).Returns(all.AsQueryable());
            //act 
            var foundCountDown = fakeTestService.GetTestCountDownByTestId(testId);
            //assert

            Assert.AreEqual(expectedCountDown, foundCountDown);
        }

        [TestMethod]
        public void GetRandomTest_Throw_OnCategoryIdEmpty()
        {
            //arrange & act & assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeTestService.GetRandomTestFromCategory(Guid.Empty));
        }

        [TestMethod]
        public void GetRandomTest_Calls_RepoAll()
        {

            //arrange
            var all = new List<Test>()
            {
                new Test()
                {
                    CategoryId = correctGuid, Status = TestStatus.Published, IsDeleted = false
                }
            };
            repoTestMock.Setup(x => x.All).Returns(all.AsQueryable);

            //act
            fakeTestService.GetRandomTestFromCategory(correctGuid);

            //assert
            repoTestMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetRandomTest_GetsTheCorrectTest()
        {

            //arrange
            var correctTest = new Test()
            {
                CategoryId = correctGuid,
                Status = TestStatus.Published,
                IsDeleted = false//only this is ok
            };
            var correctTestDto = new TestDTO()
            {
                CategoryId = correctGuid,
                Status = TestStatus.Published,
                IsDeleted = false//only this is ok
            };
            var all = new List<Test>()
            {
                correctTest,
                new Test()
                {
                    CategoryId = correctGuid, Status = TestStatus.Draft, IsDeleted = false
                },
                new Test()
                {
                    CategoryId = correctGuid, Status = TestStatus.Disabled, IsDeleted = false
                },
                new Test()
                {
                    CategoryId = correctGuid, Status = TestStatus.Published, IsDeleted = true
                },
                new Test()
                {
                    CategoryId = correctGuid, Status = TestStatus.Draft, IsDeleted = true
                },
                new Test()
                {
                    CategoryId = correctGuid, Status = TestStatus.Disabled, IsDeleted = true
                }
            };
            repoTestMock.Setup(x => x.All).Returns(all.AsQueryable);
            mapperMock.Setup(x => x.MapTo<TestDTO>(correctTest)).Returns(correctTestDto);

            //act
            var foundTest = fakeTestService.GetRandomTestFromCategory(correctGuid);

            //assert
            Assert.AreEqual(correctTestDto, foundTest);
            Assert.AreSame(correctTestDto, foundTest);
        }


        [TestMethod]
        public void GetRandomTest_Throw_WhenCategoriesEmpty()
        {
            //arrange 
            var all = new List<Test>();
            repoTestMock.Setup(x => x.All).Returns(all.AsQueryable);

            //act & assert
            Assert.ThrowsException<CategoryEmptyException>(() => fakeTestService.GetRandomTestFromCategory(correctGuid));
        }

        [TestMethod]
        public void GetRandomTest_Calls_RandomGiveRandom()
        {

            //arrange
            var all = new List<Test>()
            {
                new Test()
                {
                    CategoryId = correctGuid, Status = TestStatus.Published, IsDeleted = false
                }
            };
            repoTestMock.Setup(x => x.All).Returns(all.AsQueryable);

            //act
            fakeTestService.GetRandomTestFromCategory(correctGuid);

            //assert
            randomMock.Verify(x => x.GiveMeRandomNumber(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetRandomTest_Calls_MapperMap()
        {
            //arrange
            var all = new List<Test>()
            {
                new Test()
                {
                    CategoryId = correctGuid, Status = TestStatus.Published, IsDeleted = false
                }
            };
            repoTestMock.Setup(x => x.All).Returns(all.AsQueryable);

            //act
            fakeTestService.GetRandomTestFromCategory(correctGuid);

            //assert
            mapperMock.Verify(x => x.MapTo<TestDTO>(It.IsAny<Test>()), Times.Once);
        }

        [TestMethod]
        public void GetTestById_Throw_WhenIdEmpty()
        {
            //arrange & act & assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeTestService.GetTestById(Guid.Empty));

        }

        [TestMethod]
        public void GetTestById_Calls_RepoAll()
        {
            //arrange
            var all = new List<Test>()
            {
                new Test()
                {
                    Id = correctGuid
                }
            };
            repoTestMock.Setup(x => x.All).Returns(all.AsQueryable());

            //act
            fakeTestService.GetTestById(correctGuid);

            //assert
            repoTestMock.Verify(x => x.All, Times.Once);
        }
        [TestMethod]
        public void GetTestById_Calls_MapperMap()
        {
            //arrange
            var all = new List<Test>()
            {
                new Test()
                {
                    Id = correctGuid
                }
            };
            repoTestMock.Setup(x => x.All).Returns(all.AsQueryable);

            //act
            fakeTestService.GetTestById(correctGuid);

            //assert
            mapperMock.Verify(x => x.MapTo<TestDTO>(It.IsAny<Test>()), Times.Once);
        }
        [TestMethod]
        public void GetTestById_ReturnsCorrectTest()
        {
            //arrange
            var expectedTest = new Test()
            {
                Id = correctGuid
            };
            var expectedTestDto = new TestDTO()
            {
                Id = correctGuid
            };

            var all = new List<Test>()
            {
                expectedTest,
                new Test()
                {
                    Id = wrongGuid
                }
            };
            repoTestMock.Setup(x => x.All).Returns(all.AsQueryable);
            mapperMock.Setup(x => x.MapTo<TestDTO>(expectedTest)).Returns(expectedTestDto);

            //act
            var foundTest = fakeTestService.GetTestById(correctGuid);

            //assert
            Assert.AreEqual(expectedTestDto, foundTest);
            Assert.AreSame(expectedTestDto, foundTest);
        }

        [TestMethod]
        public void GetTestByName_Throw_WhenIdEmpty()
        {
            //arrange & act & assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeTestService.GetTestByName(null));

        }

        [TestMethod]
        public void GetTestByName_Calls_RepoAll()
        {
            //arrange
            var testName = "Mitko The Boss";
            var all = new List<Test>()
            {
                new Test()
                {
                    Name=testName
                }
            };
            repoTestMock.Setup(x => x.All).Returns(all.AsQueryable());

            //act
            fakeTestService.GetTestByName(testName);

            //assert
            repoTestMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetTestByName_Calls_MapperMap()
        {
            //arrange
            var testName = "Cheficha Cold";
            var all = new List<Test>()
            {
                new Test()
                {
                    Name= testName
                }
            };
            repoTestMock.Setup(x => x.All).Returns(all.AsQueryable);

            //act
            fakeTestService.GetTestByName(testName);

            //assert
            mapperMock.Verify(x => x.MapTo<TestDTO>(It.IsAny<Test>()), Times.Once);
        }

        [TestMethod]
        public void GetTestByName_ReturnsCorrectTest()
        {
            //arrange
            var testCorrectName = "Petko";
            var testWrongName = "Marto Stamatov";
            var expectedTest = new Test()
            {
                Name = testCorrectName
            };
            var expectedTestDto = new TestDTO()
            {
                Name = testCorrectName
            };

            var all = new List<Test>()
            {
                expectedTest,
                new Test()
                {   
                    Name = testWrongName
                }
            };
            repoTestMock.Setup(x => x.All).Returns(all.AsQueryable);
            mapperMock.Setup(x => x.MapTo<TestDTO>(expectedTest)).Returns(expectedTestDto);

            //act
            var foundTest = fakeTestService.GetTestByName(testCorrectName);

            //assert
            Assert.AreEqual(expectedTestDto, foundTest);
            Assert.AreSame(expectedTestDto, foundTest);
        }
    }
}
