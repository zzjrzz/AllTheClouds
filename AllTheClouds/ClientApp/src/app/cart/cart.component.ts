import {Component, OnInit} from '@angular/core';
import {CartService} from '../services/cart.service';
import {FormBuilder} from '@angular/forms';
import {Product} from '../services/product.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  items: Product[];
  checkoutForm;

  constructor(
    private cartService: CartService,
    private formBuilder: FormBuilder) {

    this.checkoutForm = this.formBuilder.group({
      customerName: '',
      customerEmail: ''
    });
  }

  ngOnInit() {
    this.items = this.cartService.getItems();
  }

  onSubmit(customerData) {
    this.items = this.cartService.clearCart();
    this.checkoutForm.reset();
  }
}
