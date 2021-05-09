import { OrderItem } from "./order-item";

export interface Cart {
  id: string;
  totalAmount: number;
  userId: string;

  items: OrderItem[];
}
