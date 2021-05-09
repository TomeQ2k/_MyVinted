namespace MyVinted.Core.Application.Logic.Responses
{
    public interface IJwtAuthorizationTokenResponse
    {
        string Token { get; init; }
    }
}