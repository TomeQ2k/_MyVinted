import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { CartService } from "../services/cart.service";
import { Notifier } from "../services/notifier.service";
import { CartResponse } from "./responses/cart-response";

@Injectable()
export class CartResolver implements Resolve<CartResponse> {
  constructor(private router: Router, private cartService: CartService,
    private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<CartResponse> {
    return this.cartService.getCart().pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
