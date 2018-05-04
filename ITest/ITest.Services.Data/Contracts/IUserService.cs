using ITest.Data.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITest.Services.Data
{
    public interface IUserService
    {
        string GetLoggedUserId(ClaimsPrincipal claims);

        Task<User> GetCurrentUser(ClaimsPrincipal claims);
    }
}