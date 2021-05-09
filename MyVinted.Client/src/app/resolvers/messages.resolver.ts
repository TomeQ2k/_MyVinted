import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { Messenger } from "../services/messenger.service";
import { Notifier } from "../services/notifier.service";
import { MessengerRequest } from "./requests/messenger-request";
import { MessagesResponse } from "./responses/messages-response";

@Injectable()
export class MessagesResolver implements Resolve<MessagesResponse> {
  constructor(private router: Router, private messenger: Messenger,
    private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<MessagesResponse> {
    return this.messenger.getMessagesThread(route.params.recipientId, new MessengerRequest()).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
