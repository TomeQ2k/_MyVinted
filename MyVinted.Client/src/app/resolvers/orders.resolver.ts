import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { Notifier } from "../services/notifier.service";
import { OrderService } from "../services/order.service";
import { OrdersRequest } from "./requests/orders-request";
import { OrdersResponse } from "./responses/orders-response";

@Injectable()
export class OrdersResolver implements Resolve<OrdersResponse> {
  constructor(private router: Router, private orderService: OrderService,
    private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<OrdersResponse> {
    return this.orderService.getOrders(new OrdersRequest()).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
