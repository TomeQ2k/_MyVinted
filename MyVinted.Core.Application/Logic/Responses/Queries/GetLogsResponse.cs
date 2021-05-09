using System.Collections.Generic;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record GetLogsResponse : BaseResponse
    {
        public IEnumerable<LogModel> Logs { get; init; }

        public GetLogsResponse(Error error = null) : base(error) { }
    }
}