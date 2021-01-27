import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ProductModel} from '../model/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  public products: ProductModel[];

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
  }

  getProductInCurrency(currency: string) {
    return this.http.get<ProductModel[]>(this.baseUrl + 'api/products?markupMultiplier=' + 1.2 + '&targetCurrency=' + currency);
  }
}
