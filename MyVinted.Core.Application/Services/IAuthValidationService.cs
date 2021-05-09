using System.Threading.Tasks;

namespace MyVinted.Core.Application.Services
{
    public interface IAuthValidationService
    {
        Task<bool> EmailExists(string email);
        Task<bool> UsernameExists(string username);
    }
}