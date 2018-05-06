using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.Services.Data;
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
    public class UserTestAnswersServiceTests
    {
        Mock<IRepository<UserTestAnswers>> repoUserTestAnswersMock;
        Mock<ISaver> saverMock;
        UserTestAnswersService fakeUserTestAnswersService;

        string correctGuidString = "495f7e92-d4ae-4299-9340-b300d4242f57";
        Guid correctGuid;
        Guid wrongGuid;

        [TestInitialize]
        public void TestInitialize()
        {
            repoUserTestAnswersMock = new Mock<IRepository<UserTestAnswers>>();
            saverMock = new Mock<ISaver>();

            correctGuid = Guid.Parse("495f7e92-d4ae-4299-9340-b300d4242f57");
            correctGuidString = "495f7e92-d4ae-4299-9340-b300d4242f57";

            wrongGuid = Guid.Parse("2b932e9d-95e3-48db-8fbd-dee921dc6795");

            fakeUserTestAnswersService = new UserTestAnswersService(repoUserTestAnswersMock.Object, saverMock.Object);
        }

        [TestMethod]
        public void SaveQuestionAnswers_ThrowWhenUserTestsDtoNull()
        {
            //arrange & act & assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeUserTestAnswersService.SaveQuestionAnswers(null));
        }

        [TestMethod]
        public void SaveQuestionAnswers_Calls_RepoAdd()
        {
            //arrange 
            var fakeUserTestsDto = new UserTestsDTO
            {
                Questions = new Collection<QuestionDTO>
                {
                    new QuestionDTO()
                }
            };
            //act 
            fakeUserTestAnswersService.SaveQuestionAnswers(fakeUserTestsDto);
            //assert
            repoUserTestAnswersMock.Verify(x => x.Add(It.IsAny<UserTestAnswers>()), Times.Once);
        }

        [TestMethod]
        public void SaveQuestionAnswers_RepoAddsAllCorrect()
        {
            //arrange 
            var fakeUserTestsDto = new UserTestsDTO
            {
                Questions = new Collection<QuestionDTO>
                {
                    new QuestionDTO(),
                    new QuestionDTO(),
                    new QuestionDTO()
                }
            };
            //act 
            fakeUserTestAnswersService.SaveQuestionAnswers(fakeUserTestsDto);
            //assert
            repoUserTestAnswersMock.Verify(x => x.Add(It.IsAny<UserTestAnswers>()), Times.Exactly(3));
        }

        [TestMethod]
        public void SaveQuestionAnswers_SaverSaveChangesCalled()
        {
            //arrange 
            var fakeUserTestsDto = new UserTestsDTO
            {
                Questions = new Collection<QuestionDTO>
                {
                    new QuestionDTO(),
                    new QuestionDTO(),
                    new QuestionDTO()
                }
            };
            //act 
            fakeUserTestAnswersService.SaveQuestionAnswers(fakeUserTestsDto);
            //assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once);
        }
        [TestMethod]
        public void GetResult_ThrowWhenUserIdNull()
        {
            //arrange 
            var categoryName = "Mitko The Boss";

            // act & assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeUserTestAnswersService.GetResult(null, categoryName));
        }
        [TestMethod]
        public void GetResult_ThrowWhenCategoryNameIsNull()
        {
            //arrange 
            var userId = correctGuidString;
            //act & assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeUserTestAnswersService.GetResult(userId, null));
        }
        [TestMethod]
        public void GetResult_CallRepoAll()
        {
            //arrange 
            var userId = correctGuidString;
            var categoryname = "Cheficha Cold";
            //act
            fakeUserTestAnswersService.GetResult(userId, categoryname);
            // assert
            repoUserTestAnswersMock.Verify(x => x.All, Times.Once);
        }
        [TestMethod]
        public void GetResult_ReturnZeroIfNoUserTestAnswers()
        {
            //arrange 
            var userId = correctGuidString;
            var categoryname = "Cheficha Cold";
            decimal expectedResult = 0;
            //act
            var foundResult = fakeUserTestAnswersService.GetResult(userId, categoryname);
            // assert
            Assert.AreEqual(expectedResult, foundResult);
        }
        [TestMethod]
        public void GetResult_GivesCorrectResult()
        {
            //1 correct answer from 3 questions
            //arrange 
            var userId = correctGuidString;
            var categoryname = "Cheficha Cold";
            var userTest = new UserTests
            {
                UserId = userId,
                Test = new Test
                {
                    Category = new Category
                    {
                        Name = categoryname
                    },
                    Questions = new Collection<Question>
                            {
                                new Question(),
                                new Question(),
                                new Question()
                            }
                }

            };
            decimal expectedResult = 33.33m;
            //(uta => uta.UserTest.UserId == userId && uta.UserTest.Test.Category.Name == category
            var all = new List<UserTestAnswers>
            {
                new UserTestAnswers
                {
                    UserTest = userTest,
                    Answer=new Answer
                    {
                        Correct=true
                    }
                },
                new UserTestAnswers
                {
                    UserTest = userTest,
                     Answer = new Answer
                     {
                         Correct=false
                     }
                },
                new UserTestAnswers
                {
                    UserTest = userTest,
                     Answer = new Answer
                     {
                        Correct=false
                     }
                }
            }.AsQueryable();

            repoUserTestAnswersMock.Setup(x => x.All).Returns(all);
            //act
            var foundResult = fakeUserTestAnswersService.GetResult(userId, categoryname);
            // assert
            Assert.AreEqual(expectedResult, foundResult);
        }
        [TestMethod]
        public void RecalculateAllTakenTestsWithName_ThrowWhenTestName()
        {
            //arrange & act & assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeUserTestAnswersService.RecalculateAllTakenTestsWithName(null));
            
        }
    }
}
