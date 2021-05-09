import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddCartItemRequest } from '../resolvers/requests/add-cart-item-request';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private readonly cartApiUrl = environment.apiUrl + 'cart/';

  constructor(private httpClient: HttpClient) { }

  public getCart() {
    return this.httpClient.get<any>(this.cartApiUrl);
  }

  public addToCart(request: AddCartItemRequest) {
    return this.httpClient.post(this.cartApiUrl + 'add', request);
  }

  public removeFromCart(itemId: string) {
    return this.httpClient.delete(this.cartApiUrl + 'remove', { params: { itemId } });
  }

  public clearCart() {
    return this.httpClient.delete(this.cartApiUrl + 'clear');
  }
}
