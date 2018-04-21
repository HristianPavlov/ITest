using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace ITest.Services.Data
{
    public class TestRandomService:ITestRandomService
    {
        private readonly IMappingProvider mapper;
        private readonly IRepository<Test> tests;

        public TestRandomService(IMappingProvider mapper, IRepository<Test> tests)
        {
            this.mapper = mapper;
            this.tests = tests;
        }
        public TestDTO GetRandomTestFromCategory(int categoryID)
        {
            //must fix it to give random test (the testid must be some random number < tests.count)
            var testsFromThisCategory = tests.All.Where(test => test.CategoryId == categoryID).
                                                        Include(t => t.Questions).ThenInclude(x => x.Answers);
            var randomTest = testsFromThisCategory.First();
            var randomTestDto = mapper.MapTo<TestDTO>(randomTest); 
            return randomTestDto;
        }
    }
}
