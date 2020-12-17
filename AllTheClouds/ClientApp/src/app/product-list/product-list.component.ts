import {Component, OnInit} from '@angular/core';
import {productSamples} from '../productSamples';
import {CartService} from '../services/cart.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products = productSamples;

  constructor(private cartService: CartService) {
  }

  ngOnInit() {
  }

  addToCart(product) {
    this.cartService.addToCart(product);
    alert('Your cloud has been added to the cart!');
  }
}
