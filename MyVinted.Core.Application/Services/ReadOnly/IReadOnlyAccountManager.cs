using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyAccountManager
    {
        Task<User> GetCurrentUser();
    }
}