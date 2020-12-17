import {Injectable} from '@angular/core';
import {Order, OrderService} from './order.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  items = [];

  constructor() {
  }

  addToCart(product) {
    this.items.push(product);
  }

  getItems() {
    return this.items;
  }

  clearCart() {
    this.items = [];
    return this.items;
  }
}
