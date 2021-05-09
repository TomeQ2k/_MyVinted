using System.Threading.Tasks;

namespace MyVinted.Core.Application.Services
{
    public interface IResetPasswordManager
    {
        Task<bool> ResetPassword(string email, string newPassword, string token);
        Task<bool> SendResetPasswordCallback(string email, string newPassword);
    }
}