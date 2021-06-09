using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses
{
    public record BaseResponse : IBaseResponse
    {
        public bool IsSucceeded { get; init; }

        public Error Error { get; init; }

        public BaseResponse(Error error = null)
            => (Error, IsSucceeded) = (error, error == null);
    }
}