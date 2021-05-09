import { PaginationRequest } from "./pagination-request";

export class OrdersRequest extends PaginationRequest {
  sortType: number = 0;
  validatedStatus: number = 0;
  login: string;
}
