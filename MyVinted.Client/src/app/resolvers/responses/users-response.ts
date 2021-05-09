import { User } from "src/app/models/domain/user";
import { BaseResponse } from "./base-response";

export class UsersResponse extends BaseResponse {
  users: User[];
}
