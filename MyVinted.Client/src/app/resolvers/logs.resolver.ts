import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { LogsService } from "../services/logs.service";
import { Notifier } from "../services/notifier.service";
import { LogsRequest } from "./requests/logs-request";
import { LogsResponse } from "./responses/logs-response";

@Injectable()
export class LogsResolver implements Resolve<LogsResponse> {
  constructor(private router: Router, private logsService: LogsService,
    private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<LogsResponse> {
    return this.logsService.getLogs(new LogsRequest()).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
