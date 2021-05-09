import { OfferPhoto } from "./offer-photo";

export interface OfferUpdate {
  id: string;
  title: string;
  price: number;
  description: string;
  allowBidding: boolean;
  categoryId: string;
  ownerId: string;

  offerPhotos: OfferPhoto[];
}
