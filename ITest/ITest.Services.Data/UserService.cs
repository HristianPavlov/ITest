using ITest.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITest.Services.Data
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public UserService()
        {
                
        }

        public string GetLoggedUserId(ClaimsPrincipal claims)
        {
            return this.userManager.GetUserId(claims);
        }

        public Task<User> GetCurrentUser(ClaimsPrincipal claims)
        {
            return this.userManager.GetUserAsync(claims);
        }
    }
}
