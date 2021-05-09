using System.Collections.Generic;

namespace MyVinted.Core.Domain.Data.Models
{
    public interface IPagedList<T> : IEnumerable<T>
    {
        int CurrentPage { get; }
        int TotalPages { get; }
        int PageSize { get; }
        int TotalCount { get; }
    }
}