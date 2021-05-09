using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Logic.Requests.Queries
{
    public abstract record PaginationRequest
    {
        protected const int MaxPageSize = int.MaxValue;
        protected const int MinPageNumber = 1;

        protected int pageNumber = MinPageNumber;
        public int PageNumber
        {
            get => pageNumber;
            init => pageNumber = (value < MinPageNumber) ? MinPageNumber : value;
        }

        protected int pageSize = Constants.PageSize;
        public int PageSize
        {
            get => pageSize;
            init => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}