using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Extensions
{
    public static class CollectionExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> collection, int pageNumber, int pageSize) where T : class
            => PagedList<T>.Create(collection, pageNumber, pageSize);

        public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> collection, int pageNumber, int pageSize) where T : class
            => await PagedList<T>.CreateAsync(collection, pageNumber, pageSize);
    }
}