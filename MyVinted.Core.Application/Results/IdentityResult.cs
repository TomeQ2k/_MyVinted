using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Results
{
    public record IdentityResult
    (
        string Token,
        User User
    );
}