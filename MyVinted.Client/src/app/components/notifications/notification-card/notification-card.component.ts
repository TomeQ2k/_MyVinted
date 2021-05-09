import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Notification } from 'src/app/models/domain/notification';
import { NotificationService } from 'src/app/services/notification.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-notification-card',
  templateUrl: './notification-card.component.html',
  styleUrls: ['./notification-card.component.scss']
})
export class NotificationCardComponent {
  @Input() notification: Notification;

  @Output() notificationRead = new EventEmitter<string>();
  @Output() notificationDeleted = new EventEmitter<string>();

  constructor(private notificationService: NotificationService, private notifier: Notifier) { }

  public markAsRead() {
    this.notificationService.markAsRead(this.notification.id).subscribe(() => {
      this.notificationRead.emit(this.notification.id);
      this.notificationService.decrementCurrentUnreadNotificationsCount();
    }, error => this.notifier.push(error, 'error'));
  }

  public removeNotification() {
    this.notificationService.removeNotification(this.notification.id).subscribe(() => {
      this.notificationDeleted.emit(this.notification.id);

      if (!this.notification.isRead) {
        this.notificationService.decrementCurrentUnreadNotificationsCount();
      }
    }, error => this.notifier.push(error, 'error'));
  }
}
