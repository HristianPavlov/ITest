
using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using Microsoft.EntityFrameworkCore;
using System;
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
    }
}
