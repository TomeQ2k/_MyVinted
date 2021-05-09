import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { Notifier } from "../services/notifier.service";
import { OfferService } from "../services/offer.service";
import { OffersRequest } from "./requests/offers-request";
import { OffersResponse } from "./responses/offers-response";

@Injectable()
export class OffersResolver implements Resolve<OffersResponse> {
  constructor(private router: Router, private offerService: OfferService,
    private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<OffersResponse> {
    return this.offerService.getOffers(new OffersRequest()).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
