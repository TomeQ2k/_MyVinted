import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { NotificationService } from "../services/notification.service";
import { Notifier } from "../services/notifier.service";
import { NotificationsResponse } from "./responses/notifications-response";

@Injectable()
export class NotificationsResolver implements Resolve<NotificationsResponse> {
  constructor(private router: Router, private notificationService: NotificationService,
    private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<NotificationsResponse> {
    return this.notificationService.getNotifications().pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
