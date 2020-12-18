import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {CartService} from '../services/cart.service';
import {ProductService} from '../services/product.service';
import {CurrencyModel} from '../model/currency.model';
import {ProductModel} from '../model/product.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, OnChanges {
  products: ProductModel[];
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
     this.cartService.addToCart(product, this.quantity);
  }

  setQuantity(quantity: number) {
    this.quantity = quantity;
  }
}
