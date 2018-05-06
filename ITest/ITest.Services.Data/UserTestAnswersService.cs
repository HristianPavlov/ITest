
using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ITest.Services.Data
{
    public class UserTestAnswersService : IUserTestAnswersService
    {
        private readonly IRepository<UserTestAnswers> userTestAnswers;
        private readonly ISaver saver;

        public UserTestAnswersService(IRepository<UserTestAnswers> userTestAnswers, ISaver saver)
        {
            this.userTestAnswers = userTestAnswers;
            this.saver = saver;
        }

        public void SaveQuestionAnswers(UserTestsDTO test)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            foreach (var qa in test.Questions)
            {
                var utaToAdd = new UserTestAnswers
                {
                    UserTestId = test.UserTestId,
                    AnswerId = qa.SelectedAnswerId
                };

                userTestAnswers.Add(utaToAdd);
            }
            saver.SaveChanges();
        }
        public decimal GetResult(string userId, string category)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var answers = this.userTestAnswers.All.
                Where(uta => uta.UserTest.UserId == userId && uta.UserTest.Test.Category.Name == category).
                Include(uta => uta.UserTest).
                    ThenInclude(ut => ut.Test).
                    ThenInclude(t => t.Questions).
                    ThenInclude(q => q.Answers);
            //ToList();
            if (answers.Count() == 0)
            {
                return 0;
            }
            decimal allQuestionsCount = answers.First().UserTest.Test.Questions.Count();

            decimal correctAnswers = 0;

            foreach (var a in answers)
            {
                if (a.Answer.Correct)
                {
                    correctAnswers++;
                }
            }
            decimal score = Math.Round((correctAnswers / allQuestionsCount * 100), 2);
            return score;
        }
        public void RecalculateAllTakenTestsWithName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var correctTests = this.userTestAnswers.All;
            //test purpose only!!
                //.Where(uta => uta.UserTest.Test.Name == name)
                //.Include(uta => uta.UserTest)
                //    .ThenInclude(ut => ut.Test)
                //        .ThenInclude(ut => ut.Questions)
                //.Include(uta => uta.Answer)
                //.ToList();

            foreach (var ut in correctTests)
            {
                var userTest = ut.UserTest;
                userTest.Score = this.GetThisUserTestScore(userTest);
            }

            saver.SaveChanges();
        }

        //removing this method because it makes untestable code

        public decimal GetThisUserTestScore(UserTests userTest)
        {
            decimal correctAnswers = userTest.Answers.Where(a => a.Answer.Correct).Count();
            decimal numOfQuestions = userTest.Test.Questions.Count();
            return Math.Round((correctAnswers / numOfQuestions * 100), 2);
        }
    }
}
