import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { AccountService } from "../services/account.service";
import { Notifier } from "../services/notifier.service";
import { ProfileResponse } from "./responses/profile-response";

@Injectable()
export class ProfileResolver implements Resolve<ProfileResponse> {
  constructor(private router: Router, private accountService: AccountService,
    private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<ProfileResponse> {
    return this.accountService.getProfile().pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
