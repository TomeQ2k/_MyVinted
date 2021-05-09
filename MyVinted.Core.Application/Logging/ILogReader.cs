using System.Threading.Tasks;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logging
{
    public interface ILogReader
    {
        Task<PagedList<LogModel>> GetLogsFromFile(GetLogsRequest request);
    }
}