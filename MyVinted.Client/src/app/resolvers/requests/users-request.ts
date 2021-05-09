import { PaginationRequest } from "./pagination-request";

export class UsersRequest extends PaginationRequest {
  name: string;
  onlyVerified: boolean = false;
  followStatus: number = 0;

  sortType: number = 0;
}
