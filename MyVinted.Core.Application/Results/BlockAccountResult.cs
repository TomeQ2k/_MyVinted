namespace MyVinted.Core.Application.Results
{
    public record BlockAccountResult
    (
        bool IsSucceeded,
        bool IsBlocked
    );
}