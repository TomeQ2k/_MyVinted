import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/helpers/pagination';
import { OffersRequest } from '../resolvers/requests/offers-request';
import { FavoritesOffersResponse } from '../resolvers/responses/favorites-offers-response';

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {
  private readonly favoritesApiUrl = environment.apiUrl + 'favorites/';

  constructor(private httpClient: HttpClient) { }

  public getFavoritesOffers(offersRequest: OffersRequest) {
    const paginatedResult: PaginatedResult<FavoritesOffersResponse> = new PaginatedResult<FavoritesOffersResponse>();

    const httpParams = this.createFavoritesRequestParams(offersRequest);

    return this.httpClient.get<FavoritesOffersResponse>(this.favoritesApiUrl + 'filter', { observe: 'response', params: httpParams })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination')) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }

          return paginatedResult;
        })
      );
  }

  public likeOffer(offerId: string) {
    return this.httpClient.put(this.favoritesApiUrl + 'like', { offerId }, { observe: 'response' });
  }

  private createFavoritesRequestParams(offersRequest: OffersRequest) {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', offersRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', offersRequest.pageSize.toString());

    if (offersRequest.title) {
      httpParams = httpParams.append('title', offersRequest.title);
    }

    if (offersRequest.categoryId) {
      httpParams = httpParams.append('categoryId', offersRequest.categoryId);
    }

    httpParams = httpParams.append('onlyVerified', offersRequest.onlyVerified.toString());
    httpParams = httpParams.append('boughtOfferStatus', offersRequest.boughtOfferStatus.toString());
    httpParams = httpParams.append('sortType', offersRequest.sortType.toString());

    return httpParams;
  }
}
