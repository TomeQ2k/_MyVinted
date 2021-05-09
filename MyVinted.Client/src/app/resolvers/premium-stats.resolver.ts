import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { Notifier } from "../services/notifier.service";
import { StatsService } from "../services/stats.service";
import { StatsResponse } from "./responses/stats-response";

@Injectable()
export class PremiumStatsResolver implements Resolve<StatsResponse> {
  constructor(private router: Router, private statsService: StatsService,
    private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<StatsResponse> {
    return this.statsService.fetchPremiumStats().pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
