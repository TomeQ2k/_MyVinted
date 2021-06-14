namespace MyVinted.Core.Application.Results
{
    public record LikeMessageResult
    (
        bool IsSucceeded,
        bool IsLiked
    );
}