import {Component, OnInit} from '@angular/core';
import {FormBuilder} from '@angular/forms';

@Component({
  selector: 'app-currency',
  templateUrl: './currency.component.html',
  styleUrls: ['./currency.component.css']
})
export class CurrencyComponent implements OnInit {
  currencyForm;

  constructor(private formBuilder: FormBuilder
  ) {
    this.currencyForm = this.formBuilder.group({
      currency: ''
    });
  }

  ngOnInit() {
  }

  onSubmit(currency) {
  }
}
