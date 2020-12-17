import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  public products: Product[];

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
  }

  getProducts() {
    return this.http.get<Product[]>(this.baseUrl + 'api/products');
  }

  getProductInCurrency(currency: string) {
    return this.http.get<Product[]>(this.baseUrl + 'api/products/' + currency);
  }
}

export interface Product {
  productId: string;
  name: string;
  description: string;
  unitPrice: number;
  maxQuantity?: number;
}
