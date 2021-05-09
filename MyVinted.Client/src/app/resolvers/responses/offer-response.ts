import { Offer } from "src/app/models/domain/offer";
import { BaseResponse } from "./base-response";

export class OfferResponse extends BaseResponse {
  offer: Offer;
}
