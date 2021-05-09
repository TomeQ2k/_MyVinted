namespace MyVinted.Core.Application.Results
{
    public record FacebookUserResult
    (
        string Id,
        string Email,
        string FirstName,
        string LastName,
        string PictureUrl
    );
}