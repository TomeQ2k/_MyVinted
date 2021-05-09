import { PaginationRequest } from "./pagination-request";

export class OffersRequest extends PaginationRequest {
  title: string;
  categoryId: string;
  onlyVerified: boolean = false;
  boughtOfferStatus: number = 1;

  sortType: number = 0;
}
