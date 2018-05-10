using ITest.Data;
using ITest.Data.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTests.UnitOfWork
{
    [TestClass]
    public class EfSaverTests
    {
        [TestMethod]
        public void ContextCallsSaveChanges()
        {
            //arrange
            var mockContext = new Mock<ITestDbContext>();
            var fakeSaver = new EFSaver(mockContext.Object);

            //act
            fakeSaver.SaveChanges();

            //assert
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
