import { OfferUpdate } from "src/app/models/domain/offer-update";
import { BaseResponse } from "./base-response";

export class OfferUpdateResponse extends BaseResponse {
  offerToUpdate: OfferUpdate;
}
