import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { Notifier } from "../services/notifier.service";
import { OfferService } from "../services/offer.service";
import { OfferResponse } from "./responses/offer-response";

@Injectable()
export class OfferResolver implements Resolve<OfferResponse> {
  constructor(private router: Router, private offerService: OfferService,
    private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<OfferResponse> {
    return this.offerService.getOffer(route.params.offerId).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
