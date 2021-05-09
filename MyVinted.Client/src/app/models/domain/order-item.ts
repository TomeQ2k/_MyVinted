import { OrderType } from "src/app/enums/order-type.enum";

export interface OrderItem {
  id: string;
  amount: number;
  type: OrderType;
  productName: string;
  userName: string;
  email: string;
  orderId: string;
  cartId: string;
}
