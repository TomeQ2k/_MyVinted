using System.Threading.Tasks;
using MyVinted.Core.Application.Results;

namespace MyVinted.Core.Application.Services
{
    public interface IUserManager
    {
        Task<BlockAccountResult> ToggleBlockAccount(string userId);
    }
}