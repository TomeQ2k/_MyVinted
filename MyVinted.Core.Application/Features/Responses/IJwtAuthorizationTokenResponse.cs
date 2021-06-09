namespace MyVinted.Core.Application.Features.Responses
{
    public interface IJwtAuthorizationTokenResponse
    {
        string Token { get; init; }
    }
}