import { OrderItem } from "./order-item";

export interface Order {
  id: string;
  dateCreated: Date;
  totalAmount: number;
  isValidated?: boolean;
  userId?: string;

  showItems: boolean;

  items: OrderItem[];
}
