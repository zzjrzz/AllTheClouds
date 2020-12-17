import {Component, OnInit} from '@angular/core';
import {CartService} from '../services/cart.service';
import {Product, ProductService} from '../services/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[];

  constructor(private cartService: CartService, private productService: ProductService) {
  }

  ngOnInit() {
    this.productService.getProductInCurrency('USD')
      .subscribe(
        products => (this.products = products),
        error => (console.log(error))
      );
  }

  addToCart(product) {
    this.cartService.addToCart(product);
    alert('Your cloud has been added to the cart!');
  }
}
