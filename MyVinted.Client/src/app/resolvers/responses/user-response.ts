import { User } from "src/app/models/domain/user";
import { BaseResponse } from "./base-response";

export class UserResponse extends BaseResponse {
  user: User;
}
