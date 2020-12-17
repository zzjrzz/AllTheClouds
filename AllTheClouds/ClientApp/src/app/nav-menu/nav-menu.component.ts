import {Component, EventEmitter, Output} from '@angular/core';
import {CurrencyModel} from '../model/currency.model';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  public selectedCurrency: CurrencyModel;
  @Output() currencyChanged = new EventEmitter<CurrencyModel>();

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  setCurrency(currency: string) {
    this.currencyChanged.emit(new CurrencyModel(currency));
  }
}
