import { Component, OnInit } from '@angular/core';
import { productSamples } from '../productSamples';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products = productSamples;

  constructor() { }

  ngOnInit() {
  }

}
