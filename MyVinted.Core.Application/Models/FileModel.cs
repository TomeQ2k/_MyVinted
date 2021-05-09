namespace MyVinted.Core.Application.Models
{
    public record FileModel
    (
        string Path,
        string Url,
        string FullPath = null
    );
}