using System;
using MyVinted.Core.Application.Helpers;

namespace MyVinted.Core.Application.Models
{
    public record LogModel
    (
        DateTime Date,
        string Message,
        string Level = LogLevel.Information,
        string Exception = null,
        string RequestMethod = null,
        string RequestPath = null,
        int? StatusCode = null,
        float? Elapsed = null,
        string SourceContext = null,
        string RequestId = null,
        string ConnectionId = null
    );
}