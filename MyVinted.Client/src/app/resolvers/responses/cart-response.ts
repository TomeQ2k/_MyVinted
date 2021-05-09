import { Cart } from "src/app/models/domain/cart";
import { BaseResponse } from "./base-response";

export class CartResponse extends BaseResponse {
  cart: Cart;
}
