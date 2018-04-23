using ITest.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ITest.Services.Data
{
    public class UserService
    {
        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public string GetLoggedUserId(ClaimsPrincipal claims)
        {
            return this.userManager.GetUserId(claims);
        }
    }
}
