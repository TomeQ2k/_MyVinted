import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { hubNames } from 'src/environments/environment';
import { UserAuth } from './models/domain/user-auth';
import { AuthService } from './services/auth.service';
import { Listener } from './services/listener.service';
import { Messenger } from './services/messenger.service';
import { NotificationService } from './services/notification.service';
import { Notifier } from './services/notifier.service';
import { Signalr, SIGNALR_ACTIONS } from './services/signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  private jwtHelper = new JwtHelperService();

  constructor(private authService: AuthService, private listener: Listener, private notificationService: NotificationService, private messenger: Messenger,
    private notifier: Notifier, private signalr: Signalr) {
    window.onbeforeunload = () => {
      this.signalr.closeConnection(hubNames.notifier);
      this.signalr.closeConnection(hubNames.messenger);
    };
  }

  ngOnInit(): void {
    this.validateAuthorization();

    this.authService.currentSignedIn.subscribe(isSignedIn => {
      if (isSignedIn) {
        this.subscribeSignalr();
      }
    });

    if (this.authService.isSignedIn()) {
      this.notificationService.countUnreadNotifications();
      this.messenger.countUnreadMessages();
    }
  }

  private validateAuthorization() {
    const token = localStorage.getItem('token');
    const user: UserAuth = JSON.parse(localStorage.getItem('user'));

    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }

    if (user) {
      this.authService.currentUser = user;
      this.listener.changeCurrentUser(user);
    }
    if (token && user && this.validateTokenExpiration(token)) {
      this.authService.isSignedInEmit();
    }
  }

  private validateTokenExpiration(token: any): boolean {
    if (this.jwtHelper.isTokenExpired(token)) {
      this.authService.signOut();
      this.notifier.push('Authorization token expired. Please sign in again', 'warning');

      return false;
    }

    return true;
  }

  private subscribeSignalr() {
    if (this.authService.isSignedIn()) {
      this.signalr.startConnection(hubNames.notifier);
      this.signalr.startConnection(hubNames.messenger);

      this.signalr.subscribeAction(SIGNALR_ACTIONS.NOTIFICATION_RECEIVED, hubNames.notifier, values => {
        this.notificationService.countUnreadNotifications();
        this.notifier.push(`Notification: ${values[0].text}`);
      });

      this.signalr.subscribeAction(SIGNALR_ACTIONS.MESSAGE_RECEIVED, hubNames.messenger, () => {
        this.messenger.countUnreadMessages();
      });
    }
  }
}
