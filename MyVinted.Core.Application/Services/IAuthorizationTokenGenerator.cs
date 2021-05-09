using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IAuthorizationTokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}