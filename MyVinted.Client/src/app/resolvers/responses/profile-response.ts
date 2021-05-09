import { UserProfile } from "src/app/models/domain/user-profile";
import { BaseResponse } from "./base-response";

export class ProfileResponse extends BaseResponse {
  userProfile: UserProfile;
}
