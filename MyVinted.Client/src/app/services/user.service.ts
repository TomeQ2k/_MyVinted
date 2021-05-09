import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/helpers/pagination';
import { UsersRequest } from '../resolvers/requests/users-request';
import { UsersResponse } from '../resolvers/responses/users-response';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly userApiUrl = environment.apiUrl + 'user/';

  constructor(private httpClient: HttpClient) { }

  public getUser(userId: string) {
    return this.httpClient.get<any>(this.userApiUrl, { params: { userId } });
  }

  public getUsers(usersRequest: UsersRequest) {
    const paginatedResult: PaginatedResult<UsersResponse> = new PaginatedResult<UsersResponse>();

    const httpParams = this.createUsersRequestParams(usersRequest);

    return this.httpClient.get<UsersResponse>(this.userApiUrl + 'filter', { observe: 'response', params: httpParams })
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

  public followUser(userId: string) {
    return this.httpClient.put(this.userApiUrl + 'follow', { userId }, { observe: 'response' });
  }

  public toggleBlockAccount(userId: string) {
    return this.httpClient.patch(this.userApiUrl + 'block/toggle', { userId }, { observe: 'response' });
  }

  private createUsersRequestParams(usersRequest: UsersRequest) {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', usersRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', usersRequest.pageSize.toString());

    if (usersRequest.name) {
      httpParams = httpParams.append('name', usersRequest.name);
    }

    httpParams = httpParams.append('followStatus', usersRequest.followStatus.toString());
    httpParams = httpParams.append('onlyVerified', usersRequest.onlyVerified.toString());
    httpParams = httpParams.append('sortType', usersRequest.sortType.toString());

    return httpParams;
  }
}
