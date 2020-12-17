import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {CartService} from '../services/cart.service';
import {ProductService} from '../services/product.service';
import {CurrencyModel} from '../model/currency.model';
import {Product} from '../model/product.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, OnChanges {
  products: Product[];
  quantity: number;
  @Input() currencyChangeEvent: CurrencyModel;

  constructor(private cartService: CartService, private productService: ProductService) {
  }

  ngOnChanges(changes: SimpleChanges) {
    this.productService.getProductInCurrency(this.currencyChangeEvent.selectedCurrency)
      .subscribe(
        products => (this.products = products),
        error => (console.log(error))
      );
  }

  ngOnInit() {
    this.productService.getProductInCurrency(this.currencyChangeEvent.selectedCurrency)
      .subscribe(
        products => (this.products = products),
        error => (console.log(error))
      );
  }

  addToCart(product) {
    for (let i = 0; i < this.quantity; i++) {
      this.cartService.addToCart(product);
    }
  }

  setQuantity(quantity: number) {
    this.quantity = quantity;
  }
}
