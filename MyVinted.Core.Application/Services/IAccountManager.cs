using Microsoft.AspNetCore.Http;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Services
{
    public interface IAccountManager : IReadOnlyAccountManager
    {
        Task<bool> ChangeUsername(string newUsername);
        Task<bool> ChangePassword(string oldPassword, string newPassword);

        Task<bool> ChangeEmail(string newEmail, string token);
        Task<bool> SendChangeEmailCallback(string newEmail);

        Task<bool> ChangePhoneNumber(string newPhoneNumber);

        Task<string> SetAvatar(IFormFile avatar);
        Task<bool> DeleteAvatar();
    }
}