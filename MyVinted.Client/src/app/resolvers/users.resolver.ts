import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { Notifier } from "../services/notifier.service";
import { UserService } from "../services/user.service";
import { UsersRequest } from "./requests/users-request";
import { UsersResponse } from "./responses/users-response";

@Injectable()
export class UsersResolver implements Resolve<UsersResponse> {
  constructor(private router: Router, private userService: UserService,
    private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<UsersResponse> {
    return this.userService.getUsers(new UsersRequest()).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
