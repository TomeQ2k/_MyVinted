import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/helpers/pagination';
import { MessengerRequest } from '../resolvers/requests/messenger-request';
import { ConversationsResponse } from '../resolvers/responses/conversation-response';
import { MessagesResponse } from '../resolvers/responses/messages-response';

@Injectable({
  providedIn: 'root'
})
export class Messenger {
  private readonly messengerApiUrl = environment.apiUrl + 'messenger/';

  private unreadMessagesCount = new BehaviorSubject<number>(0);
  currentUnreadMessagesCount = this.unreadMessagesCount.asObservable();

  constructor(private httpClient: HttpClient) { }

  public getConversations(messengerRequest: MessengerRequest) {
    const paginatedResult: PaginatedResult<ConversationsResponse> = new PaginatedResult<ConversationsResponse>();

    const httpParams = this.createMessengerRequestParams(messengerRequest);

    return this.httpClient.get<ConversationsResponse>(this.messengerApiUrl + 'conversations', { observe: 'response', params: httpParams })
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

  public getMessagesThread(recipientId: string, messengerRequest: MessengerRequest) {
    const paginatedResult: PaginatedResult<MessagesResponse> = new PaginatedResult<MessagesResponse>();

    let httpParams = this.createMessengerRequestParams(messengerRequest);
    httpParams = httpParams.append('recipientId', recipientId);

    return this.httpClient.get<MessagesResponse>(this.messengerApiUrl + 'thread', { observe: 'response', params: httpParams })
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

  public sendMessage(request: any) {
    return this.httpClient.post(this.messengerApiUrl + 'send', request, { observe: 'response' });
  }

  public deleteMessage(messageId: string) {
    return this.httpClient.delete(this.messengerApiUrl + 'delete', { params: { messageId } });
  }

  public likeMessage(messageId: string) {
    return this.httpClient.patch(this.messengerApiUrl + 'like', { messageId });
  }

  public readMessage(messageId: string) {
    return this.httpClient.patch(this.messengerApiUrl + 'read', { messageId });
  }

  public countUnreadMessages() {
    return this.httpClient.get<any>(this.messengerApiUrl + 'unread/count').subscribe(response => {
      if (response.isSucceeded) {
        this.changeCurrentUnreadMessagesCount(response.unreadMessagesCount);
      }
    });
  }

  public changeCurrentUnreadMessagesCount(unreadNotificationsCount: number) {
    this.unreadMessagesCount.next(unreadNotificationsCount);
  }

  public decrementCurrentUnreadMessagesCount() {
    this.unreadMessagesCount.next(this.unreadMessagesCount.value - 1);
  }

  private createMessengerRequestParams(messengerRequest: MessengerRequest) {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', messengerRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', messengerRequest.pageSize.toString());

    if (messengerRequest.userName) {
      httpParams = httpParams.append('userName', messengerRequest.userName);
    }

    return httpParams;
  }
}
