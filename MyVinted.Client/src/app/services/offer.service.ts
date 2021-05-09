import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/helpers/pagination';
import { OffersRequest } from '../resolvers/requests/offers-request';
import { OffersResponse } from '../resolvers/responses/offers-response';

@Injectable({
  providedIn: 'root'
})
export class OfferService {
  private readonly offerApiUrl = environment.apiUrl + 'offer/';

  constructor(private httpClient: HttpClient) { }

  public getOffer(offerId: string) {
    return this.httpClient.get<any>(this.offerApiUrl, { params: { offerId } });
  }

  public getOffers(offersRequest: OffersRequest) {
    const paginatedResult: PaginatedResult<OffersResponse> = new PaginatedResult<OffersResponse>();

    const httpParams = this.createOffersRequestParams(offersRequest);

    return this.httpClient.get<OffersResponse>(this.offerApiUrl + 'filter', { observe: 'response', params: httpParams })
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

  public getOfferToUpdate(offerId: string) {
    return this.httpClient.get<any>(this.offerApiUrl + 'update', { params: { offerId } });
  }

  public getMyOffers(offersRequest: OffersRequest) {
    const paginatedResult: PaginatedResult<OffersResponse> = new PaginatedResult<OffersResponse>();

    const httpParams = this.createOffersRequestParams(offersRequest);

    return this.httpClient.get<OffersResponse>(this.offerApiUrl + 'my/filter', { observe: 'response', params: httpParams })
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

  public addOffer(request: any) {
    const formData = new FormData();

    formData.append('title', request.title);
    formData.append('price', request.price.toString());
    formData.append('description', request.description);
    formData.append('allowBidding', request.allowBidding.toString());
    formData.append('categoryId', request.categoryId);

    Array.from(request.photos).map((photo: File) => {
      return formData.append('photos', photo, photo.name);
    });

    return this.httpClient.post(this.offerApiUrl + 'add', formData);
  }

  public updateOffer(request: any) {
    const formData = new FormData();

    formData.append('offerId', request.offerId);
    formData.append('title', request.title);
    formData.append('price', request.price.toString());
    formData.append('description', request.description);
    formData.append('allowBidding', request.allowBidding.toString());
    formData.append('categoryId', request.categoryId);

    Array.from(request.photos).map((photo: File) => {
      return formData.append('photos', photo, photo.name);
    });

    return this.httpClient.put(this.offerApiUrl + 'update', formData);
  }

  public deleteOffer(offerId: string) {
    return this.httpClient.delete(this.offerApiUrl + 'delete', { params: { offerId } });
  }

  public deleteOfferPhoto(photoId: string, offerId: string) {
    return this.httpClient.delete(this.offerApiUrl + 'photos/delete', { params: { photoId, offerId } });
  }

  private createOffersRequestParams(offersRequest: OffersRequest) {
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
