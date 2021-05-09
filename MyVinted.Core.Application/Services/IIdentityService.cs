using System.Threading.Tasks;
using MyVinted.Core.Application.Results;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IIdentityService
    {
        Task<IdentityResult> SignIn(string email, string password);
        Task<User> SignUp(string email, string username, string password);

        Task<bool> ConfirmAccount(string email, string token);

        Task<string> GenerateConfirmAccountToken(User user);
    }
}