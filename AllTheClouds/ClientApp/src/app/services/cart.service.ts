import {Injectable} from '@angular/core';
import {ProductModel} from '../model/product.model';
import {CartItemModel} from '../model/cart-item.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  orderItems: CartItemModel[] = [];

  constructor() {
  }

  addToCart(product: ProductModel, quantity: number) {
    const index = this.orderItems.findIndex((orderItem) => orderItem.product === product);
    if (index < 0) {
      this.orderItems.push(new CartItemModel(product, quantity));
    } else {
      this.orderItems[index].quantity += +quantity;
    }
  }

  getItems() {
    return this.orderItems;
  }

  clearCart() {
    this.orderItems = [];
    return this.orderItems;
  }

  delete(i: number) {
    this.orderItems.splice(i, 1);
    return this.orderItems;
  }
}
