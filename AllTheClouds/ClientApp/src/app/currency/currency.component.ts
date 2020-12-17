import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CurrencyModel} from '../model/currency.model';

@Component({
  selector: 'app-currency',
  templateUrl: './currency.component.html',
  styleUrls: ['./currency.component.css']
})

export class CurrencyComponent implements OnInit {

  @Output() currencyChanged = new EventEmitter<CurrencyModel>();
  public selectedCurrency: CurrencyModel;

  constructor() {
  }

  ngOnInit() {
    this.selectedCurrency = new CurrencyModel('AUD');
  }

  currencySelected(currency: string) {
    this.selectedCurrency = new CurrencyModel(currency);
    this.currencyChanged.emit(this.selectedCurrency);
  }
}
