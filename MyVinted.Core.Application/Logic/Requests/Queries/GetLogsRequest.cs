using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Logic.Requests.Queries
{
    public record GetLogsRequest : PaginationRequest, IRequest<GetLogsResponse>
    {
        public DateTime Date { get; init; } = DateTime.Now.AddDays(-1);

        public string Message { get; init; }
        public string Level { get; init; }
        public string RequestMethod { get; init; }
        public string RequestPath { get; init; }
        public int? StatusCode { get; init; }
        public string Exception { get; init; }
        public DateTime? MinTime { get; init; }
        public DateTime? MaxTime { get; init; }

        public LogSortType SortType { get; init; }

        public GetLogsRequest()
        {
            PageSize = Constants.LogsPageSize;
        }
    }

    public class GetLogsRequestValidator : AbstractValidator<GetLogsRequest>
    {
        public GetLogsRequestValidator()
        {
            RuleFor(x => x.Date).NotNull().Must(x => x <= DateTime.Now.AddDays(-1));
        }
    }
}