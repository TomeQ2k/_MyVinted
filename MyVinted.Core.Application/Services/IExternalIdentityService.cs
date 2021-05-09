using System.Threading.Tasks;
using MyVinted.Core.Application.Results;

namespace MyVinted.Core.Application.Services
{
    public interface IExternalIdentityService
    {
        Task<IdentityResult> SignInWithExternalProvider(string provider, string idToken);
    }
}