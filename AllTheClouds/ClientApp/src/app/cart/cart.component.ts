import {Component, OnInit} from '@angular/core';
import {CartService} from '../services/cart.service';
import {FormBuilder} from '@angular/forms';
import {CartItemModel} from '../model/cart-item.model';
import {OrderService} from '../services/order.service';
import {OrderItem} from '../model/order-item.dto';
import {Order} from '../model/order.dto';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  orderItems: CartItemModel[];
  checkoutForm;

  constructor(
    private cartService: CartService,
    private orderService: OrderService,
    private formBuilder: FormBuilder) {

    this.checkoutForm = this.formBuilder.group({
      customerName: '',
      customerEmail: ''
    });
  }

  ngOnInit() {
    this.orderItems = this.cartService.getItems();
  }

  onSubmit(customerData) {
    const lineItems = [];
    for (const item of this.orderItems) {
      lineItems.push(new OrderItem(item.product.productId, item.quantity));
    }

    const orderRequest = new Order(customerData.customerName, customerData.customerEmail, lineItems);
    this.orderService.sendOrder(orderRequest).subscribe();

    this.orderItems = this.cartService.clearCart();
    this.checkoutForm.reset();
  }

  removeItem(index: number) {
    this.cartService.delete(index);
  }
}
