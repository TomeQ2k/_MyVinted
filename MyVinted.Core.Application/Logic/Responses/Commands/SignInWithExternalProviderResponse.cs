using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record SignInWithExternalProviderResponse : BaseResponse, IJwtAuthorizationTokenResponse
    {
        public string Token { get; init; }
        public UserAuthDto User { get; init; }

        public SignInWithExternalProviderResponse(Error error = null) : base(error) { }
    }
}