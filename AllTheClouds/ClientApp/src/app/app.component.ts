import {Component, OnInit} from '@angular/core';
import {CurrencyModel} from './model/currency.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  private selectedCurrency: CurrencyModel;

  ngOnInit() {
    this.selectedCurrency = new CurrencyModel('AUD');
  }

  setCurrencyChange(event: CurrencyModel) {
    this.selectedCurrency = event;
  }
}
