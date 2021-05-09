import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { FavoritesService } from "../services/favorites.service";
import { Notifier } from "../services/notifier.service";
import { OffersRequest } from "./requests/offers-request";
import { FavoritesOffersResponse } from "./responses/favorites-offers-response";

@Injectable()
export class FavoritesOffersResolver implements Resolve<FavoritesOffersResponse> {
  constructor(private router: Router, private favoritesService: FavoritesService,
    private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<FavoritesOffersResponse> {
    return this.favoritesService.getFavoritesOffers(new OffersRequest()).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
