import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/helpers/pagination';
import { LogsRequest } from '../resolvers/requests/logs-request';
import { LogsResponse } from '../resolvers/responses/logs-response';

@Injectable({
  providedIn: 'root'
})
export class LogsService {
  private readonly logsApiUrl = environment.apiUrl + 'log/';

  constructor(private httpClient: HttpClient) { }

  public getLogs(logsRequest: LogsRequest) {
    const paginatedResult: PaginatedResult<LogsResponse> = new PaginatedResult<LogsResponse>();

    const httpParams = this.createUsersRequestParams(logsRequest);

    return this.httpClient.get<LogsResponse>(this.logsApiUrl + 'filter', { observe: 'response', params: httpParams })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination')) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }

          return paginatedResult;
        })
      );
  }

  private createUsersRequestParams(logsRequest: LogsRequest) {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', logsRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', logsRequest.pageSize.toString());

    httpParams = httpParams.append('date', logsRequest.date.toISOString());

    if (logsRequest.message) {
      httpParams = httpParams.append('message', logsRequest.message);
    }

    if (logsRequest.level) {
      httpParams = httpParams.append('level', logsRequest.level);
    }

    if (logsRequest.requestMethod) {
      httpParams = httpParams.append('requestMethod', logsRequest.requestMethod);
    }

    if (logsRequest.requestPath) {
      httpParams = httpParams.append('requestPath', logsRequest.requestPath);
    }

    if (logsRequest.statusCode) {
      httpParams = httpParams.append('statusCode', logsRequest.statusCode.toString());
    }

    if (logsRequest.exception) {
      httpParams = httpParams.append('exception', logsRequest.exception);
    }

    if (logsRequest.minTime) {
      httpParams = httpParams.append('minTime', logsRequest.minTime.toISOString());
    }

    if (logsRequest.maxTime) {
      httpParams = httpParams.append('maxTime', logsRequest.maxTime.toISOString());
    }

    httpParams = httpParams.append('sortType', logsRequest.sortType.toString());

    return httpParams;
  }
}
