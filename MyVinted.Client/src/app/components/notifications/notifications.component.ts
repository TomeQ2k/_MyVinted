import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Notification } from 'src/app/models/domain/notification';
import { Listener } from 'src/app/services/listener.service';
import { NotificationService } from 'src/app/services/notification.service';
import { Notifier } from 'src/app/services/notifier.service';
import { Signalr, SIGNALR_ACTIONS } from 'src/app/services/signalr.service';
import { hubNames } from 'src/environments/environment';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss']
})
export class NotificationsComponent implements OnInit {
  notifications: Notification[];

  constructor(private notificationService: NotificationService, private notifier: Notifier, private route: ActivatedRoute, private listener: Listener,
    private signalr: Signalr) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => this.notifications = data.notificationsResponse.notifications);
    this.listener.changeCurrentNavbarFormVisible(false);
    this.signalr.subscribeAction(SIGNALR_ACTIONS.NOTIFICATION_RECEIVED, hubNames.notifier, values => this.notifications.unshift(values[0]));
  }

  public markAllAsRead() {
    this.notificationService.markAllAsRead().subscribe(() => {
      this.notifications.forEach(n => n.isRead = true);
      this.notificationService.changeCurrentUnreadNotificationsCount(0);
    }, error => this.notifier.push(error, 'error'));
  }

  public clearAll() {
    if (confirm('Are you sure you want to clear all notifications?')) {
      this.notificationService.clearAllNotifications().subscribe(() => {
        this.notifier.push('Notifications were cleared');
        this.notifications = [];
        this.notificationService.changeCurrentUnreadNotificationsCount(0);
      }, error => this.notifier.push(error, 'error'));
    }
  }

  public onNotificationRead(notificationId: string) {
    const notificationIndex = this.notifications.findIndex(n => n.id === notificationId);
    this.notifications[notificationIndex] = { ...this.notifications[notificationIndex], isRead: true };
  }

  public onNotificationDeleted(notificationId: string) {
    this.notifications = this.notifications.filter(n => n.id !== notificationId);
  }
}
