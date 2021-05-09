using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses
{
    public interface IBaseResponse
    {
        bool IsSucceeded { get; init; }

        Error Error { get; init; }
    }
}