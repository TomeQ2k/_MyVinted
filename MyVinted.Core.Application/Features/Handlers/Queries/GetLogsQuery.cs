using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Features.Requests.Queries;
using MyVinted.Core.Application.Features.Responses.Queries;
using MyVinted.Core.Application.Logging;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Queries
{
    public class GetLogsQuery : IRequestHandler<GetLogsRequest, GetLogsResponse>
    {
        private readonly ILogReader logReader;
        private readonly IHttpContextWriter httpContextWriter;

        public GetLogsQuery(ILogReader logReader, IHttpContextWriter httpContextWriter)
        {
            this.logReader = logReader;
            this.httpContextWriter = httpContextWriter;
        }

        public async Task<GetLogsResponse> Handle(GetLogsRequest request, CancellationToken cancellationToken)
        {
            var logs = await logReader.GetLogsFromFile(request);

            httpContextWriter.AddPagination(logs.CurrentPage, logs.PageSize, logs.TotalCount, logs.TotalPages);

            return new GetLogsResponse {Logs = logs};
        }
    }
}