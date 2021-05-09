import { OfferList } from "./offer-list";
import { Opinion } from "./opinion";
import { UserFollow } from "./user-follow";

export interface User {
  id: string;
  email: string;
  userName: string;
  emailConfirmed: boolean;
  phoneNumber: string;
  avatarUrl: string;
  isVerified: boolean;
  isAdmin: boolean;
  followsCount: number;
  opinionsCount: number;
  rating: number;
  isBlocked: boolean;

  offers: OfferList[];
  followings: UserFollow[];
  opinions: Opinion[];
}
