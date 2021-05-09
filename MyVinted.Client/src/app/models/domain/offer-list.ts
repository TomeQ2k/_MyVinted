import { OfferLike } from "./offer-like";

export interface OfferList {
  id: string;
  title: string;
  price: number;
  description: string;
  dateUpdated: Date;
  isVerified: boolean;
  isBought: boolean;
  categoryId: string;
  categoryName: string;
  ownerId: string;
  ownerUserName: string;
  ownerEmail: string;
  ownerPhoneNumber: string;
  firstPhotoUrl: string;
  likesCount: number;
  isBooked: boolean;

  offerLikes: OfferLike[];
}
