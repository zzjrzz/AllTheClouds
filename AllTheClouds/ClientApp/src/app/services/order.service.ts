import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

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

  sendOrder(order: Order): Observable<Order> {
    return this.http.post<Order>('/api/Orders', order, httpOptions);
  }
}

export interface Order {
  customerName: string;
  customerEmail: string;
  lineItems: OrderItem [];
}

export interface OrderItem {
  productId: string;
  quantity: number;
}
