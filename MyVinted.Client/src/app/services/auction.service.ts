import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuctionService {
  private readonly auctionApiUrl = environment.apiUrl + 'auction/';

  constructor(private httpClient: HttpClient) { }

  public createOfferAuction(request: any) {
    return this.httpClient.post(this.auctionApiUrl + 'create', request, { observe: 'response' });
  }

  public acceptOfferAuction(auctionId: string, offerId: string) {
    return this.httpClient.put(this.auctionApiUrl + 'accept', { auctionId, offerId });
  }

  public denyOfferAuction(auctionId: string, offerId: string) {
    return this.httpClient.delete(this.auctionApiUrl + 'deny', { params: { auctionId, offerId } });
  }
}
