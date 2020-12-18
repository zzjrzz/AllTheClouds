import {OrderItem} from './order-item.dto';

export class Order {
  customerName: string;
  customerEmail: string;
  lineItems: OrderItem [];

  constructor(customerName: string, customerEmail: string, lineItems: OrderItem[]) {
    this.customerName = customerName;
    this.customerEmail = customerEmail;
    this.lineItems = lineItems;
  }
}
