using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace MyVinted.Core.Application.Services
{
    public interface IGoogleTokenManager
    {
        Task<Payload> VerifyGoogleToken(string idToken);
    }
}