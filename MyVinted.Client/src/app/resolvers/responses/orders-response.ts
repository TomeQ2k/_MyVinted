import { Order } from "src/app/models/domain/order";
import { BaseResponse } from "./base-response";

export class OrdersResponse extends BaseResponse {
  orders: Order[];
}
