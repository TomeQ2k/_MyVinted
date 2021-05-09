using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Logging;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Models;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Application.SmartEnums;

namespace MyVinted.Infrastructure.Persistence.Logging
{
    public class LogReader : ILogReader
    {
        private readonly IReadOnlyFilesManager filesManager;
        private readonly LogKeyWordsDictionary logKeyWordsDictionary;

        public LogReader(IReadOnlyFilesManager filesManager, LogKeyWordsDictionary logKeyWordsDictionary)
        {
            this.filesManager = filesManager;
            this.logKeyWordsDictionary = logKeyWordsDictionary;
        }

        public async Task<PagedList<LogModel>> GetLogsFromFile(GetLogsRequest request)
        {
            var logsFilePath = BuildLogFilesPath(request.Date);

            if (!filesManager.FileExists(logsFilePath))
                return new List<LogModel>().ToPagedList<LogModel>(request.PageNumber, request.PageSize);

            string[] logsJson = await filesManager.ReadFileLines(logsFilePath);
            ReplaceKeyWordsInJson(ref logsJson);

            var logs = ConvertLogsFileIntoList(logsJson);

            logs = FilterLogs(request, logs);

            return logs.ToPagedList<LogModel>(request.PageNumber, request.PageSize);
        }

        #region private

        private string BuildLogFilesPath(DateTime date) =>
            $"/logs/log-{date.Year}{(date.Month < 10 ? $"0{date.Month}" : date.Month)}{(date.Day < 10 ? $"0{date.Day}" : date.Day)}.txt";

        private static IEnumerable<LogModel> ConvertLogsFileIntoList(string[] logsJson)
        {
            foreach (var logJson in logsJson)
                yield return logJson.FromJSON<LogModel>();
        }

        private static IEnumerable<LogModel> FilterLogs(GetLogsRequest request, IEnumerable<LogModel> logs)
        {
            if (!string.IsNullOrEmpty(request.Message))
                logs = logs.Where(l => l.Message != null && l.Message.ToLower().Contains(request.Message.ToLower()));

            if (!string.IsNullOrEmpty(request.Level))
                logs = logs.Where(l => l.Level != null && l.Level.ToUpper().Contains(request.Level.ToUpper()));

            if (!string.IsNullOrEmpty(request.RequestMethod))
                logs = logs.Where(l =>
                    l.RequestMethod != null && l.RequestMethod.ToLower().Contains(request.RequestMethod.ToLower()));

            if (!string.IsNullOrEmpty(request.RequestPath))
                logs = logs.Where(l =>
                    l.RequestPath != null && l.RequestPath.ToLower().Contains(request.RequestPath.ToLower()));

            if (request.StatusCode != null)
                logs = logs.Where(l => l.StatusCode == request.StatusCode);

            if (!string.IsNullOrEmpty(request.Exception))
                logs = logs.Where(l => l.Exception != null && l.Exception.ToLower().Contains(request.Exception.ToLower()));

            if (request.MinTime != null)
                logs = logs.Where(l => l.Date >= request.MinTime);

            if (request.MaxTime != null)
                logs = logs.Where(l => l.Date <= request.MaxTime);

            logs = LogSortTypeSmartEnum.FromValue((int)request.SortType).Sort(logs);

            return logs;
        }

        private void ReplaceKeyWordsInJson(ref string[] logsJson)
        {
            for (int i = 0; i < logsJson.Length; i++)
            {
                logsJson[i] = logsJson[i].Replace(LogKeyWordsDictionary.DateKey,
                    logKeyWordsDictionary[LogKeyWordsDictionary.DateKey]);
                logsJson[i] = logsJson[i].Replace(LogKeyWordsDictionary.MessageKey,
                    logKeyWordsDictionary[LogKeyWordsDictionary.MessageKey]);
                logsJson[i] = logsJson[i].Replace(LogKeyWordsDictionary.LevelKey,
                    logKeyWordsDictionary[LogKeyWordsDictionary.LevelKey]);
                logsJson[i] = logsJson[i].Replace(LogKeyWordsDictionary.ExceptionKey,
                    logKeyWordsDictionary[LogKeyWordsDictionary.ExceptionKey]);
            }
        }

        #endregion
    }
}