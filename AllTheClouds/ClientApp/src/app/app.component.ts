import {Component, EventEmitter, Output, SimpleChange} from '@angular/core';
import {CurrencyModel} from './model/currency.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  private selectedCurrency: CurrencyModel;

  ngOnInit() {
    this.selectedCurrency = new CurrencyModel('AUD');
  }

  setCurrencyChange(event: CurrencyModel) {
    this.selectedCurrency = event;
  }
}
