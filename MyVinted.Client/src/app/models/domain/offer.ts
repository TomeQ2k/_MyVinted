import { OfferAuction } from "./offer-auction";
import { OfferLike } from "./offer-like";
import { OfferPhoto } from "./offer-photo";

export interface Offer {
  id: string;
  title: string;
  price: number;
  description: string;
  allowBidding: boolean;
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

  offerAuction: OfferAuction;

  offerPhotos: OfferPhoto[];
  offerLikes: OfferLike[];
}
