import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateTokenCardData, StripeCardElement } from '@stripe/stripe-js';
import { StripeService } from 'ngx-stripe';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/helpers/pagination';
import { OrdersRequest } from '../resolvers/requests/orders-request';
import { PaymentRequest } from '../resolvers/requests/payment-request';
import { OrdersResponse } from '../resolvers/responses/orders-response';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private readonly orderApiUrl = environment.apiUrl + 'order/';

  constructor(private httpClient: HttpClient, private stripeService: StripeService) { }

  public getOrders(ordersRequest: OrdersRequest) {
    const paginatedResult: PaginatedResult<OrdersResponse> = new PaginatedResult<OrdersResponse>();

    const httpParams = this.createOrdersRequestParams(ordersRequest);

    return this.httpClient.get<OrdersResponse>(this.orderApiUrl + 'filter', { observe: 'response', params: httpParams })
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

  public getUserOrders(sortType: number = 0) {
    return this.httpClient.get<any>(this.orderApiUrl + 'user/filter', { params: { sortType: sortType.toString() } })
  }

  public purchaseOrder(request: PaymentRequest) {
    return this.httpClient.post(this.orderApiUrl + 'purchase', request, { observe: 'response' });
  }

  public createToken(card: StripeCardElement, tokenData: CreateTokenCardData) {
    return this.stripeService.createToken(card, tokenData);
  }

  private createOrdersRequestParams(ordersRequest: OrdersRequest) {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', ordersRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', ordersRequest.pageSize.toString());

    httpParams = httpParams.append('validatedStatus', ordersRequest.validatedStatus.toString());

    if (ordersRequest.login) {
      httpParams = httpParams.append('login', ordersRequest.login);
    }

    httpParams = httpParams.append('sortType', ordersRequest.sortType.toString());

    return httpParams;
  }
}
