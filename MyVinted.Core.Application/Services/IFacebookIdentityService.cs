using System.Threading.Tasks;
using MyVinted.Core.Application.Results;

namespace MyVinted.Core.Application.Services
{
    public interface IFacebookIdentityService : IExternalIdentityService
    {
        Task<FacebookUserResult> GetUserFromFacebook(string idToken);
    }
}