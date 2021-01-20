import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Order} from '../model/order.dto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
  }

  sendOrder(order: Order) {
    return this.http.post<Order>('api/orders', JSON.stringify(order), httpOptions);
  }
}

