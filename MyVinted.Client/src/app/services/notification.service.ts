import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Listener } from './listener.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private readonly notifierApiUrl = environment.apiUrl + 'notification/';

  private unreadNotificationsCount = new BehaviorSubject<number>(0);
  currentUnreadNotificationsCount = this.unreadNotificationsCount.asObservable();

  constructor(private httpClient: HttpClient, private listener: Listener) { }

  public getNotifications() {
    return this.httpClient.get<any>(this.notifierApiUrl + 'all');
  }

  public markAsRead(notificationId: string) {
    return this.httpClient.patch(this.notifierApiUrl + 'read', { notificationId });
  }

  public markAllAsRead() {
    return this.httpClient.put(this.notifierApiUrl + 'readAll', {});
  }

  public removeNotification(notificationId: string) {
    return this.httpClient.delete(this.notifierApiUrl + 'remove', { params: { notificationId } });
  }

  public clearAllNotifications() {
    return this.httpClient.delete(this.notifierApiUrl + 'clearAll');
  }

  public countUnreadNotifications() {
    return this.httpClient.get<any>(this.notifierApiUrl + 'unread/count').subscribe(response => {
      if (response.isSucceeded) {
        this.changeCurrentUnreadNotificationsCount(response.unreadNotificationsCount);
      }
    });
  }

  public changeCurrentUnreadNotificationsCount(unreadNotificationsCount: number) {
    this.unreadNotificationsCount.next(unreadNotificationsCount);
  }

  public decrementCurrentUnreadNotificationsCount() {
    this.unreadNotificationsCount.next(this.unreadNotificationsCount.value - 1);
  }
}
