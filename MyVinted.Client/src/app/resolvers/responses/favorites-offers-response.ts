import { OfferList } from "src/app/models/domain/offer-list";
import { BaseResponse } from "./base-response";

export class FavoritesOffersResponse extends BaseResponse {
  favoritesOffers: OfferList[];
}
