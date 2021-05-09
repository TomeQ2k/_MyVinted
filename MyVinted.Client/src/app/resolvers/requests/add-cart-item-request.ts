import { OrderType } from "src/app/enums/order-type.enum";

export interface AddCartItemRequest {
  amount: number;
  type: OrderType;
  productName: string;
  userName: string;
  email: string;

  offerId?: string;
  offerOwnerId?: string;
}
