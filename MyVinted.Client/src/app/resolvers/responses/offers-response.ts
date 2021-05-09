import { OfferList } from "src/app/models/domain/offer-list";
import { BaseResponse } from "./base-response";

export class OffersResponse extends BaseResponse {
  offers: OfferList[];
}
