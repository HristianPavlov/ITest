using ITest.Data;
using ITest.Data.Models;
using ITest.Data.Providers;
using ITest.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;
using System.Linq;

namespace DataTests.Repository
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void RepoAllWithoutDeleted_ReturnsCorrect()
        {
            // Arrange
            var stubDateProvider = new Mock<IRepoTimeProvider>();
            var options = new DbContextOptionsBuilder<ITestDbContext>()
            .UseInMemoryDatabase(databaseName: "RepoAllWithoutDeleted_ReturnsCorrect")
            .Options;
            var stubContext = new ITestDbContext(options);
            var stubRepo = new EfRepository<Answer>(stubContext, stubDateProvider.Object);

            stubRepo.Add(new Answer() { Correct = true });
            stubRepo.Add(new Answer() { Correct = false });
            stubRepo.Add(new Answer() { Correct = false });
            stubRepo.Add(new Answer() { Correct = true, IsDeleted = true });
            stubRepo.Add(new Answer() { Correct = false, IsDeleted = true });
            stubRepo.Add(new Answer() { Correct = false, IsDeleted = true });
            stubContext.SaveChanges();

            //act
            var foundAnswers = stubRepo.All;

            //assert
            Assert.AreEqual(3, foundAnswers.Count());
            Assert.AreEqual(1, foundAnswers.Where(x => x.Correct == true).Count());
            Assert.AreEqual(2, foundAnswers.Where(x => x.Correct == false).Count());
        }

        [TestMethod]
        public void RepoAllWithDeletedOnes_ReturnsCorrect()
        {
            // Arrange
            var stubDateProvider = new Mock<IRepoTimeProvider>();
            var options = new DbContextOptionsBuilder<ITestDbContext>()
            .UseInMemoryDatabase(databaseName: "RepoAllWithDeletedOnes_ReturnsCorrect")
            .Options;
            var stubContext = new ITestDbContext(options);
            var stubRepo = new EfRepository<Answer>(stubContext, stubDateProvider.Object);

            stubRepo.Add(new Answer() { Content = "Petko", Correct = true });
            stubRepo.Add(new Answer() { Content = "MitkoTheEyes", Correct = false });
            stubRepo.Add(new Answer() { Content = "Edo", Correct = false, IsDeleted = true });
            stubRepo.Add(new Answer() { Content = "Stivi", Correct = false });
            stubRepo.Add(new Answer() { Content = "MartoStamatov", Correct = true, IsDeleted = true });
            stubRepo.Add(new Answer() { Content = "ChefichaCold", Correct = false, IsDeleted = true });
            stubContext.SaveChanges();

            //act
            var foundAnswers = stubRepo.AllAndDeleted;

            //assert
            Assert.AreEqual(6, foundAnswers.Count());
            Assert.AreEqual(2, foundAnswers.Where(x => x.Correct == true).Count());
            Assert.AreEqual(4, foundAnswers.Where(x => x.Correct == false).Count());
        }

        [TestMethod]
        public void RepoAdd_Adds()
        {
            // Arrange
            var stubDateProvider = new Mock<IRepoTimeProvider>();
            var options = new DbContextOptionsBuilder<ITestDbContext>()
            .UseInMemoryDatabase(databaseName: "RepoAdd_Adds")
            .Options;
            var stubContext = new ITestDbContext(options);
            var stubRepo = new EfRepository<Answer>(stubContext, stubDateProvider.Object);
            var expectedCount = 1;
            var answer = new Answer() { Content = "MitkoTheEyes" };
            stubRepo.Add(answer);

            stubContext.SaveChanges();

            //act
            var foundAnswers = stubRepo.All;

            //assert
            Assert.AreEqual(expectedCount, foundAnswers.Count());
        }
        [TestMethod]
        public void Delete_SetsDeletedToTrue()
        {
            // Arrange
            var stubDateProvider = new Mock<IRepoTimeProvider>();
            stubDateProvider.Setup(x => x.GetDateTimeNow()).Returns(new DateTime(2017, 1, 18));
            var options = new DbContextOptionsBuilder<ITestDbContext>()
            .UseInMemoryDatabase(databaseName: "Delete_SetsDeletedToTrue")
            .Options;
            var stubContext = new ITestDbContext(options);
            var stubRepo = new EfRepository<Answer>(stubContext, stubDateProvider.Object);
            var answerToBeDeteletd = new Answer() { Content = "ChefichaCold" };

            //act
            stubRepo.Add(answerToBeDeteletd);
            stubContext.SaveChanges();
            stubRepo.Delete(answerToBeDeteletd);
            stubContext.SaveChanges();
            var foundDeletedAnswer = stubRepo.AllAndDeleted.First();

            //assert
            Assert.AreEqual(true, foundDeletedAnswer.IsDeleted);
        }

        [TestMethod]
        public void Delete_SetsCorrectDateTime()
        {
            // Arrange
            var stubDateProvider = new Mock<IRepoTimeProvider>();
            var someDate = new DateTime(2017, 1, 18);
            stubDateProvider.Setup(x => x.GetDateTimeNow()).Returns(someDate);
            var options = new DbContextOptionsBuilder<ITestDbContext>()
            .UseInMemoryDatabase(databaseName: "Delete_SetsCorrectDateTime")
            .Options;
            var stubContext = new ITestDbContext(options);
            var stubRepo = new EfRepository<Answer>(stubContext, stubDateProvider.Object);
            var answerToBeDeteletd = new Answer() { Content = "ChefichaCold" };

            //act
            stubRepo.Add(answerToBeDeteletd);
            stubContext.SaveChanges();
            stubRepo.Delete(answerToBeDeteletd);
            stubContext.SaveChanges();
            var foundDeletedAnswer = stubRepo.AllAndDeleted.First();

            //assert
            Assert.AreEqual(someDate, foundDeletedAnswer.DeletedOn);
        }

        [TestMethod]
        public void Delete_RemovesEntityFromRepoAll()
        {
            // Arrange
            var stubDateProvider = new Mock<IRepoTimeProvider>();
            stubDateProvider.Setup(x => x.GetDateTimeNow()).Returns(new DateTime(2017, 1, 18));
            var options = new DbContextOptionsBuilder<ITestDbContext>()
            .UseInMemoryDatabase(databaseName: "Delete_RemovesEntityFromRepoAll")
            .Options;
            var stubContext = new ITestDbContext(options);
            var stubRepo = new EfRepository<Answer>(stubContext, stubDateProvider.Object);
            var answerToBeDeteletd = new Answer() { Content = "MitkoTheBoss" };

            //act
            stubRepo.Add(answerToBeDeteletd);
            stubContext.SaveChanges();
            stubRepo.Delete(answerToBeDeteletd);
            stubContext.SaveChanges();

            //assert
            Assert.AreEqual(0, stubRepo.All.Count());
            Assert.AreEqual(1, stubRepo.AllAndDeleted.Count());
        }

        [TestMethod]
        public void Update_UpdatesPropertiesCorrect()
        {
            // Arrange
            var stubDateProvider = new Mock<IRepoTimeProvider>();
            stubDateProvider.Setup(x => x.GetDateTimeNow()).Returns(new DateTime(2017, 1, 18));
            var options = new DbContextOptionsBuilder<ITestDbContext>()
            .UseInMemoryDatabase(databaseName: "Update_UpdatesPropertiesCorrect")
            .Options;
            var stubContext = new ITestDbContext(options);
            var stubRepo = new EfRepository<Answer>(stubContext, stubDateProvider.Object);
            var answerToBeUpdated = new Answer() { Correct = false, Content = "Petko" };

            //act
            stubRepo.Add(answerToBeUpdated);
            stubContext.SaveChanges();

            answerToBeUpdated.Correct = true;
            answerToBeUpdated.Content = "MartoStamatov";
            stubRepo.Update(answerToBeUpdated);

            //assert
            Assert.AreEqual(true, stubRepo.All.First().Correct);
            Assert.AreEqual("MartoStamatov", stubRepo.All.First().Content);
        }
        //Testing the constructor 
        [TestMethod]
        public void Constructor_ShouldThrow_WhenContextEmpty()
        {
            // Arrange
            var stubDateProvider = new Mock<IRepoTimeProvider>();
            //act & assert
            Assert.ThrowsException<ArgumentNullException>(() => new EfRepository<Category>(null, stubDateProvider.Object));
        }

        [TestMethod]
        public void Constructor_ShouldThrow_WhenDateProviderEmpty()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ITestDbContext>()
            .UseInMemoryDatabase(databaseName: "Update_UpdatesPropertiesCorrect")
            .Options;
            var stubContext = new ITestDbContext(options);

            //act & assert
            Assert.ThrowsException<ArgumentNullException>(() => new EfRepository<Category>(stubContext,null));
        }
    }
}
