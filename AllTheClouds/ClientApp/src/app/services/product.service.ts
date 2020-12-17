import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Product} from '../model/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  public products: Product[];

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
  }

  getProductInCurrency(currency: string) {
    return this.http.get<Product[]>(this.baseUrl + 'api/products/' + currency);
  }
}
