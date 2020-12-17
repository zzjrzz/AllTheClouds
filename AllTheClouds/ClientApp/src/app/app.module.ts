import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';
import {ProductListComponent} from './product-list/product-list.component';
import {CurrencyComponent} from './currency/currency.component';
import {CartComponent} from './cart/cart.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FetchDataComponent,
    ProductListComponent,
    CurrencyComponent,
    CartComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: ProductListComponent, pathMatch: 'full'},
      {path: 'cart', component: CartComponent},
      {path: 'fetch-data', component: FetchDataComponent},
    ]),
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
